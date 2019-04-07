import { IFieldModel } from "./IFieldModel";

export interface IInputValueModel {
    fields: { [field: string]: IFieldModel }
}