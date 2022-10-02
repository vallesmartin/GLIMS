export interface Entity {
    EntityId: number,
    EntityDescription: string,
    EntityLegalName: string,
    EntityAddress: string,
    EntityLocation: string,
    EntityContact: string,
    EntityPhone: string,
    EntityMail: string,
    EntityInternalCode: string,
    EntityType: number
}

export class EntityClass {
    EntityId: number = 0;
    EntityDescription: string = "";
    EntityLegalName: string = "";
    EntityAddress: string = "";
    EntityLocation: string = "";
    EntityContact: string = "";
    EntityPhone: string = "";
    EntityMail: string = "";
    EntityInternalCode: string = "";
    EntityType: number = 0;
}