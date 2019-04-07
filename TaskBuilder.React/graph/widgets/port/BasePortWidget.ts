import { BaseWidgetProps, BaseWidget } from "storm-react-diagrams";
import { BasePortModel } from "../../models/port/BasePortModel";

export interface PortWidgetProps<TModel> extends BaseWidgetProps {
    port: BasePortModel<TModel>;
}

export class BasePortWidget<TModel> extends BaseWidget<PortWidgetProps<TModel>> {
}
