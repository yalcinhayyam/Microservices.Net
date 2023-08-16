import { container } from "tsyringe";
export const registerWithContainer = (channel) => (token, event) => {
    channel.assertQueue(event);
    var handler = container.resolve(token);
    channel.consume(event, async (message) => {
        if (!message) {
            throw new Error(`Queue ${event} is not has any message! on consumer in amqp lib!`);
        }
        await handler.handle(JSON.parse(message.content.toString()));
        channel.ack(message);
    });
};
export const register = (channel) => (handler, event) => {
    channel.assertQueue(event);
    channel.consume(event, async (message) => {
        if (!message) {
            throw new Error(`Queue ${event} is not has any message! on consumer in amqp lib!`);
        }
        await handler.handle(JSON.parse(message.content.toString()));
        channel.ack(message);
    });
};
