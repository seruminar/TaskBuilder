import * as React from "react";

import { IFunctionModel } from "../models/function/IFunctionModel";

export interface IFunctionTrayItemProps {
    functionModel: IFunctionModel;
}

export class FunctionTrayItem extends React.Component<IFunctionTrayItemProps> {
    render() {
        const { displayName, typeGuid } = this.props.functionModel;

        return (
            <div
                draggable className="task-builder-tray-item"
                onDragStart={e => {
                    e.dataTransfer.setData("typeGuid", typeGuid);
                }}
            >
                <i className="icon-w-products-data-source" />
                <span>
                    {displayName}
                </span>
            </div>
        );
    }
}