const SRD = window["storm-react-diagrams"];

class BaseInputValueWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-value", props);
    }

    setValue = (key, value) => {
        if (value !== "") {
            this.props.model.filledModel.fields[key].value = value;
        }
    }

    render() {
        const fields = this.props.model.structureModel.fields;

        switch (this.props.model.inputType) {
            case "plain":
                return <div />;
            case "structureOnly":
            case "filled":
                return (
                    <div {...this.getProps()}>
                        {Object.keys(fields).map(key => {
                            const field = fields[key];

                            switch (field.type) {
                                case "text":
                                    return (
                                        <input
                                            type="text"
                                            key={key}
                                            defaultValue={this.props.model.filledModel.fields[key].value}
                                            onFocus={() => this.props.port.getParent().setLocked(true)}
                                            onBlur={() => this.props.port.getParent().setLocked(false)}
                                            onChange={e => this.setValue(key, e.target.value)}
                                        />
                                    );
                                case "dropdown":
                                    return (
                                        <select
                                            key={key}
                                            defaultValue={this.props.model.filledModel.fields[key].value}
                                            onChange={e => this.setValue(key, e.target.selectedOptions[0].textContent)}
                                        >
                                            {field.value.map((v, i) =>
                                                (
                                                    <option
                                                        key={i}
                                                        value={v}
                                                    >
                                                        {v}
                                                    </option>
                                                )
                                            )}
                                        </select>
                                    );
                            }
                        })}
                    </div>
                );
        }
    }
}