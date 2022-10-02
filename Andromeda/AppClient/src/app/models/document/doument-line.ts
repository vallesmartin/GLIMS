import { Item } from "../item/item";

export interface DocumentLine{
    DocumentLineId: number,
    DocumentId: number,
    DocumentLineItemId: number,
    DocumentLineItemDescription: string,
    DocumentLineQty: number,
    DocumentLineItemPrice: number,
    ItemData: Item
}