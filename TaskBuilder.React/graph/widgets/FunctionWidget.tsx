import * as React from "react";

import { BaseWidget, BaseWidgetProps } from "storm-react-diagrams";
import { FunctionModel } from "../models/FunctionModel";
import { InvokeWidget } from "./port/InvokeWidget";
import { InputWidget } from "./port/InputWidget";
import { DispatchWidget } from "./port/DispatchWidget";
import { OutputWidget } from "./port/OutputWidget";

export interface FunctionProps extends BaseWidgetProps {
    node: FunctionModel;
}

export class FunctionWidget extends BaseWidget<FunctionProps> {
    constructor(props: FunctionProps) {
        super("function", props);
    }

    render() {
        const { displayName, displayColor } = this.props.node.getFunction();

        return (
            <div {...this.getProps()} >
                <div className="function-shadow">
                    <div
                        className={this.bem("-title")}
                    >
                        <div
                            className={this.bem("-name")}
                            style={{ boxShadow: "inset 0 0.9em 3em " + displayColor }}
                        >
                            {displayName}
                        </div>
                    </div>
                    <div className={this.bem("-ports")}>
                        <div className={this.bem("-in")}>
                            <InvokeWidget port={this.props.node.getInvoke()} />
                            {this.props.node.getInputs().map(p =>
                                <InputWidget port={p} key={p.getID()} />
                            )}
                        </div>
                        <div className={this.bem("-out")}>
                            {this.props.node.getDispatchs().map(p =>
                                <DispatchWidget port={p} key={p.getID()} />
                            )}
                            {this.props.node.getOutputs().map(p =>
                                <OutputWidget port={p} key={p.getID()} />
                            )}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}