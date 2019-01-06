const SRD = window["storm-react-diagrams"];

class BasePortWidget extends SRD.BaseWidget {
    constructor(props) {
        super("task-builder-port", props);
    }

    getClassName() {
        let suffix = "";

        switch (this.props.model.type) {
            case "enter":
            case "input":
                suffix = this.bem("--in");
                break;
            case "leave":
            case "output":
                suffix = this.bem("--out");
                break;
        }

        return super.getClassName() + suffix;
    }

    getFlexDirection(portType: string) {
        switch (portType) {
            case "enter":
            case "input":
                return "row";
            case "leave":
            case "output":
                return "row-reverse";
        }
    }

    render() {
        return (
            <div {...this.getProps()} style={{ flexDirection: this.getFlexDirection(this.props.model.type) }}>
                <BasePortIconWidget node={this.props.model.getParent()} name={this.props.model.displayName} type={this.props.model.displayType} />
                <div className="port-name">{this.props.model.displayName}</div>
            </div>
        );
    }
}