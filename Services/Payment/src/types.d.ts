// import { FastifyInstance as FastifyInstanceBase } from "fastify";
export {};
interface IBus {
  publish: <T extends IIntegrationEvent>(
    queue: string,
    event: T
  ) => Promise<boolean>;
}
interface IIntegrationEvent {
  cratedAt: Date;
}

interface IIntegrationEventHandler<
  TIntegrationEvent extends IIntegrationEvent
> {
  handle(event: TIntegrationEvent): Promise<void>;
}

interface IContext {
  bus: IBus;
}

declare module "fastify" {
  interface FastifyInstance {
    context: IContext;
  }
}
