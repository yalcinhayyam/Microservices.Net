import "reflect-metadata";
import amqp, { Channel } from "amqplib";
import Fastify from "fastify";

const BUS = "bus";

const PAYMENT_SUCCESS_REDIRECT_URL = "https://www.google.com";
const PAYMENT_FAILURE_REDIRECT_URL = "https://www.google.com";

import {
  PAYMENT_FINISHED_EVENT,
  PaymentFinishedIntegrationEvent,
} from "./Events/PaymentFinishedIntegrationEvent/PaymentFinishedIntegrationEvent";

interface IContext {
  bus: IBus;
}

interface IBus {
  publish: <T extends IIntegrationEvent>(
    queue: string,
    event: T
  ) => Promise<boolean>;
}

const fastify = Fastify({
  logger: true,
});

import {
  PaymentStartedIntegrationEventHandler,
  PAYMENT_STARTED_EVENT,
  PaymentStartedIntegrationEvent,
  PAYMENT_INTEGRATION_EVENT_HANDLER,
} from "./Events/PaymentStartedIntegrationEvent/PaymentStartedIntegrationEvent";
import { registerWithContainer } from "./Utilities/register";
import {
  IIntegrationEvent,
  IIntegrationEventHandler,
} from "./Utilities/IntegrationEvent";
import { container } from "tsyringe";

const bootstrap = async () => {
  try {
    const connection = await amqp.connect("amqp://guest:guest@localhost:5672");
    const channel = await connection.createChannel();
    container.register<
      IIntegrationEventHandler<PaymentStartedIntegrationEvent>
    >(PAYMENT_INTEGRATION_EVENT_HANDLER, PaymentStartedIntegrationEventHandler);

    container.registerInstance<IBus>(BUS, new AmqpBusAdapter(channel));

    const subscribe = registerWithContainer(channel);
    subscribe<PaymentStartedIntegrationEvent>(
      PAYMENT_INTEGRATION_EVENT_HANDLER,
      PAYMENT_STARTED_EVENT
    );

    fastify.decorate<IContext>("context", {
      bus: container.resolve<IBus>(BUS),
    });

    // TODO: http://127.0.0.1:3000/pay?is-success=true
    fastify.get<{
      Querystring: { "is-success": boolean };
    }>("/pay", async function handler({ query }, reply): Promise<{
      "is-success": boolean;
    }> {
      var isSuccess = query["is-success"];
      if (isSuccess) {
        fastify.log.info("Payment success!");
        fastify.context.bus.publish(
          PAYMENT_FINISHED_EVENT,
          new PaymentFinishedIntegrationEvent(
            isSuccess,
            "Card",
            new Date(Date.now())
          )
        );
        return reply.redirect(PAYMENT_SUCCESS_REDIRECT_URL);
      }
      fastify.log.info("Payment failure, aborted, card-reject, etc!");
      return reply.redirect(PAYMENT_FAILURE_REDIRECT_URL);
    });

    await fastify.listen({ port: 3000 });
  } catch (err) {
    fastify.log.error(err);
    process.exit(1);
  }
};

bootstrap();

class AmqpBusAdapter implements IBus {
  constructor(private channel: Channel) {}
  publish = async <T extends IIntegrationEvent>(queue: string, event: T) => {
    await this.channel.assertQueue(queue);
    var result = this.channel.sendToQueue(
      queue,
      Buffer.from(JSON.stringify(event))
    );
    return Promise.resolve(result);
  };
}
