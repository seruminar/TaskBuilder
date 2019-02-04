const SRD = window["storm-react-diagrams"];

class BaseInputValueWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-value", props);
    }

    setValue = (key, value) => {
        if (value !== "") {
            this.props.model.filledModel.fields[key].value = [value];
            this.forceUpdate();
        }
    }

    setLocked = (locked) => {
        this.props.port.getParent().setLocked(locked);
    }

    renderField = (key, field) => {
        defaultFilledValue = this.props.model.filledModel.fields[key].value[0] || '';

        switch (field.type) {
            case "text":
                return (
                    <input
                        type="text"
                        key={key}
                        value={defaultFilledValue}
                        onFocus={() => this.setLocked(true)}
                        onInput={() => this.setLocked(true)}
                        onBlur={() => this.setLocked(false)}
                        onChange={e => this.setValue(key, e.target.value)}
                    />
                );
            case "dropdown":
                return (
                    <select
                        key={key}
                        value={defaultFilledValue}
                        onChange={e => this.setValue(key, e.target.selectedOptions[0].textContent)}
                    >
                        {field.value.map((value, index) =>
                            (
                                <option
                                    key={index}
                                    value={value}
                                >
                                    {value}
                                </option>
                            )
                        )}
                    </select>
                );
        }
    }

    render() {
        switch (this.props.model.inputType) {
            case "plain":
                return null;
            case "structureOnly":
            case "filled":
                const fields = this.props.model.structureModel.fields;

                return (
                    <div {...this.getProps()}>
                        {Object.keys(fields).map(key => this.renderField(key, fields[key]))}
                    </div>
                );
        }
    }
}