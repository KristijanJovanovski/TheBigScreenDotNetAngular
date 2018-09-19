import {AssetType, AssetCategory} from "./Asset";

export class Category {

    title : string;
    id : string;
    active : boolean;
    assetType : string;

    constructor(id :string, title :string, assetType: string, active = false,) {
        this.title = title;
        this.id = id;
        this.active = active;
        this.assetType = assetType;
    }

}

 