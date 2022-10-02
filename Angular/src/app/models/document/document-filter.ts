import { Entity } from "../entity/entity";

export class DocumentFilter{
    DocumentId: number = 0;
    DocumentNumber: string = '';
    EntityId: number = 0;
    DocumentDate: Date = new Date('1/1/0001 12:00:00 AM');
    DocumentLastUpdateAt: Date = new Date('1/1/0001 12:00:00 AM');
    DocumentStatus: number = 0;
    DocumentEndDate: Date = new Date('1/1/0001 12:00:00 AM');
}