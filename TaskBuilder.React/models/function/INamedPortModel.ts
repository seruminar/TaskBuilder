import { IPortModel } from "./IPortModel";

export interface INamedPortModel extends IPortModel {
    displayName: string;
    description: string;
}