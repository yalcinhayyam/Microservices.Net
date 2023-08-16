import { IIntegrationEvent } from "../../Utilities/IntegrationEvent";

export const PAYMENT_FINISHED_EVENT = "payment-finished";

export class PaymentFinishedIntegrationEvent implements IIntegrationEvent {
  constructor(
    public success: boolean,
    public type: string,
    public cratedAt: Date
  ) {}
}
