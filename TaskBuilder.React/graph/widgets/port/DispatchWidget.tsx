import * as React from "react";

import { BasePortWidget, PortWidgetProps } from "./BasePortWidget";
import { IDispatchModel } from "../../../models/function/IDispatchModel";
import { CallerIconWidget } from "../icon/CallerIconWidget";

export class DispatchWidget extends BasePortWidget<IDispatchModel> {
    constructor(props: PortWidgetProps<IDispatchModel>) {
        super("port-dispatch", props);
    }

    render() {
        return (
            <div {...this.getProps()}>
                <div className="port-name">{this.props.port.model.displayName}</div>
                <CallerIconWidget {...this.props} />
            </div>
        );
    }
}