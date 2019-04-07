import * as React from "react";

import { BasePortWidget, PortWidgetProps } from "./BasePortWidget";
import { IOutputModel } from "../../../models/function/IOutputModel";
import { ParameterIconWidget } from "../icon/ParameterIconWidget";

export class OutputWidget extends BasePortWidget<IOutputModel> {
    constructor(props: PortWidgetProps<IOutputModel>) {
        super("port-output", props);
    }

    render() {
        return (
            <div {...this.getProps()}>
                <div className="port-name">{this.props.port.model.displayName}</div>
                <ParameterIconWidget {...this.props} />
            </div>
        );
    }
}