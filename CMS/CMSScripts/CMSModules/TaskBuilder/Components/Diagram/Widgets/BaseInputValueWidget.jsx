const SRD = window["storm-react-diagrams"];

class BaseInputValueWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-value", props);
    }

    getClassName() {
        return super.getClassName();
    }

    setValue = (e) => {
        if (e.data !== "") {
            this.props.model.setValue(e.data);
        }
    }

    render() {
        let inputType;

        if (this.props.inputOptions && this.props.inputOptions.length) {
            inputType = "dropdown";
        } else {
            switch (this.props.inputType) {
                case "automatic":
                    switch (this.props.typeName) {
                        case "string":
                        case "int":
                            inputType = "field";
                            break;
                        default:
                            inputType = "none";
                    }
                    break;
                default:
                    inputType = this.props.inputType;
            }
        }

        switch (inputType) {
            case "dropdown":
                return (
                    <div {...this.getProps()}>
                        <select onChange={e => this.setValue(e)}>
                            {this.props.inputOptions.map((option, i) =>
                                <option value={option.value} key={option.value + i}>{option.displayName}</option>
                            )}
                        </select>
                    </div>
                );
            case "field":
                return (
                    <div {...this.getProps()}>
                        <input type="text" onInput={e => this.setValue(e)} />
                    </div>
                );
            case "none":
            default:
                return <div />;
        }
    }
}