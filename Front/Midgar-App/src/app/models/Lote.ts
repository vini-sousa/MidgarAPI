import { Events } from "./Events";

export interface Lote {
    id: number;

    name: string;

    price: number;

    initialDate?: Date;

    finalDate?: Date;

    quantity: number;

    eventId: number;

    event: Events;
}