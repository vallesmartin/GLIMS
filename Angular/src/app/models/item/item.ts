import { Entity, EntityClass } from "../entity/entity";

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

export class ItemClass {
    ItemId: number = 0;
    ItemDescription: string = "";
    ItemExternalCode: string = "";
    ItemInternalCode: string = "";
    ItemBarcode: string = "";
    ItemPrice: number = 0;
    ItemCost: number = 0;
    ItemSugg: number = 0;
    EntityId: number = 0;
    EntityData: Entity = new EntityClass();
}