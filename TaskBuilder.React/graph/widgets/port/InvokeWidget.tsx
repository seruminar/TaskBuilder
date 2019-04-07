import * as React from "react";

import { BasePortWidget, PortWidgetProps } from "./BasePortWidget";
import { CallerIconWidget } from "../icon/CallerIconWidget";
import { IInvokeModel } from "../../../models/function/IInvokeModel";

export class InvokeWidget extends BasePortWidget<IInvokeModel> {
    constructor(props: PortWidgetProps<IInvokeModel>) {
        super("port-invoke", props);
    }

    render() {
        return (
            <div {...this.getProps()}>
                <CallerIconWidget {...this.props} />
            </div>
        );
    }
}