export class PriceHistory {
    PriceHistoryId: number = 0;
    PriceHistoryDescription : string ="";
    PriceHistoryEntityId: number = 0;
    PriceHistoryEntityName: string ="";
    PriceHistoryDateFrom: Date | undefined;
    PriceHistoryDateTo: Date | undefined;
    Detail = PriceHistoryLine;
}

export class PriceHistoryLine {
    PriceHisLItemId: number = 0;
    PriceHisLItemDescription: string ="";
    PriceHisLItemInternalCode: string ="";
    PriceHisLItemExternalCode: string ="";
    PriceHisLOldCost: number = 0;
    PriceHisLNewCost: number = 0;
    PriceHisLOldPrice: number = 0;
    PriceHisLNewPrice: number = 0;
    PriceHisLSupplierDescription: string = "";
}