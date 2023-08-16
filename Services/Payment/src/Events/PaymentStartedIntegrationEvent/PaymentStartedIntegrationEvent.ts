import {
  IIntegrationEvent,
  IIntegrationEventHandler,
} from "../../Utilities/IntegrationEvent";

export const PAYMENT_STARTED_EVENT = "payment-started";
export const PAYMENT_INTEGRATION_EVENT_HANDLER = "payment-integration-event-handler"


export class PaymentStartedIntegrationEvent implements IIntegrationEvent {
  constructor(
    public orderId: string,
    public price: number,
    public cratedAt: Date
  ) {}
}

export class PaymentStartedIntegrationEventHandler
  implements IIntegrationEventHandler<PaymentStartedIntegrationEvent>
{
  handle(event: PaymentStartedIntegrationEvent): Promise<void> {
    console.log(event);
    return Promise.resolve();
  }
}
