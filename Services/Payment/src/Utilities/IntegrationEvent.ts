export interface IIntegrationEvent {
  cratedAt: Date;
}

export interface IIntegrationEventHandler<
  TIntegrationEvent extends IIntegrationEvent
> {
  handle(event: TIntegrationEvent): Promise<void>;
}
