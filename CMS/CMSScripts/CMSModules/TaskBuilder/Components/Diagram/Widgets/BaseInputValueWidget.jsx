const SRD = window["storm-react-diagrams"];

class BaseInputValueWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-value", props);
    }

    setValue = (key, value) => {
        if (value !== "") {
            switch (this.props.model.inputType) {
                case "fields":
                    this.props.model.defaultFieldsModel.fields.find(f => f.key === key).value = value;
                    break;
                case "dropdown":
                    this.props.model.defaultFieldsModel.fields[0] = { key, value };
                    break;
            }
        }
    }

    render() {
        switch (this.props.model.inputType) {
            case "plain":
                return <div />;
            case "fields":
                return (
                    <div {...this.getProps()}>
                        {this.props.model.fieldsModel.fields.map(field => {
                            // Create a switch based on field data type (string, number, float?, checkbox)
                            //let inputType;

                            return (
                                <input
                                    type="text"
                                    key={field.key}
                                    defaultValue={this.props.model.defaultFieldsModel.fields.find(f => f.key == field.key).value || field.value}
                                    onFocus={() => this.props.port.getParent().setLocked(true)}
                                    onBlur={() => this.props.port.getParent().setLocked(false)}
                                    onChange={e => this.setValue(field.key, e.target.value)}
                                />
                            )
                        }
                        )}
                    </div>
                );
            case "dropdown":
                return (
                    <div {...this.getProps()}>
                        <select
                            defaultValue={this.props.model.defaultFieldsModel.fields[0].value}
                            onChange={e => this.setValue(e.target.value, e.target.selectedOptions[0].textContent)}
                        >
                            {this.props.model.fieldsModel.fields.map(field =>
                                (
                                    <option
                                        key={field.key}
                                        value={field.key}
                                    >
                                        {field.value}
                                    </option>
                                )
                            )}
                        </select>
                    </div>
                );
        }
    }
}