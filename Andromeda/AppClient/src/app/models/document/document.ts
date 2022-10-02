import { DocumentLine } from "./doument-line";
import { Entity } from "../entity/entity";

export interface Document{
    DocumentId: number,
    DocumentLetter: string,
    DocumentNumber: string,
    EntityId: number,
    EntityData: Entity,
    DocumentDate: Date,
    DocumentPreparedAt: Date,
    DocumentBilledAt: Date,
    DocumentDeliveredAt : Date,
    DocumentLastUpdateAt: Date;
    DocumentStatus: number,
    DocumentTotalAmount: number,
    DocumentLinesQty: number,
    DocumentEndDate: Date,
    Detail: DocumentLine[]
}