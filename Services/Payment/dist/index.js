import "reflect-metadata";
import amqp from "amqplib";
import Fastify from "fastify";
const fastify = Fastify({
    logger: true,
});
import { PaymentIntegrationEventHandler, PaymentStartedEvent, } from "./Events/PaymentStartedIntegrationEvent/PaymentStartedIntegrationEvent";
import { registerWithContainer } from "./Utilities/register";
import { container } from "tsyringe";
const bootstrap = async () => {
    try {
        const connection = await amqp.connect("amqp://guest:guest@localhost:5672");
        const channel = await connection.createChannel();
        container.register(PaymentIntegrationEventHandler, PaymentIntegrationEventHandler);
        container.registerInstance(Symbol("Bus"), new AmqpBusAdapter(channel));
        const subscribe = registerWithContainer(channel);
        subscribe(PaymentIntegrationEventHandler, PaymentStartedEvent);
        fastify.decorate("context", {
            bus: container.resolve(Symbol("Bus")),
        });
        // TODO: http://127.0.0.1:3000/pay?is-success=true
        fastify.get("/pay", async function handler({ body, query }, reply) {
            if (query["is-success"]) {
                fastify.log.info("Payment success!");
                fastify.context.bus;
                return ""; // query["is-success"]
            }
            fastify.log.info("Payment failure, aborted, card-reject, etc!");
            return ""; // query["is-success"]
        });
        await fastify.listen({ port: 3000 });
    }
    catch (err) {
        fastify.log.error(err);
        process.exit(1);
    }
};
bootstrap();
class AmqpBusAdapter {
    channel;
    constructor(channel) {
        this.channel = channel;
    }
    publish = (event, queue) => {
        var result = this.channel.sendToQueue(queue, Buffer.from(JSON.stringify(event)));
        return Promise.resolve(result);
    };
}
