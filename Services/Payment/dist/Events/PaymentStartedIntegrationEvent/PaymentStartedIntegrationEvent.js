export const PaymentStartedEvent = "payment-started";
export class PaymentIntegrationEventHandler {
    handle(event) {
        console.log(event);
        return Promise.resolve();
    }
}
export class PaymentStartedIntegrationEvent {
    orderId;
    price;
    cratedAt;
    constructor(orderId, price, cratedAt) {
        this.orderId = orderId;
        this.price = price;
        this.cratedAt = cratedAt;
    }
}
