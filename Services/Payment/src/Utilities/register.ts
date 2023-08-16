import { Channel } from "amqplib";
import {
  IIntegrationEvent,
  IIntegrationEventHandler,
} from "./IntegrationEvent";
import { InjectionToken, container } from "tsyringe";

export const registerWithContainer =
  (channel: Channel) =>
  <TEvent extends IIntegrationEvent>(token: InjectionToken, event: string) => {
    channel.assertQueue(event);
    var handler = container.resolve<IIntegrationEventHandler<TEvent>>(token);
    channel.consume(event, async (message) => {
      if (!message) {
        throw new Error(
          `Queue ${event} is not has any message! on consumer in amqp lib!`
        );
      }
      await handler.handle(JSON.parse(message.content.toString()));
      channel.ack(message);
    });
  };

export const register =
  (channel: Channel) =>
  <TEvent extends IIntegrationEvent>(
    handler: IIntegrationEventHandler<TEvent>,
    event: string
  ) => {
    channel.assertQueue(event);
    channel.consume(event, async (message) => {
      if (!message) {
        throw new Error(
          `Queue ${event} is not has any message! on consumer in amqp lib!`
        );
      }
      await handler.handle(JSON.parse(message.content.toString()));
      channel.ack(message);
    });
  };
