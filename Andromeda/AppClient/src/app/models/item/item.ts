import { Entity } from "../entity/entity";

export interface Item {
    ItemId: number,
    ItemDescription: string,
    ItemExternalCode: string,
    ItemInternalCode: string,
    ItemBarcode: string,
    ItemPrice: number,
    ItemCost: number,
    ItemSugg: number,
    EntityId: number,
    EntityData: Entity
}