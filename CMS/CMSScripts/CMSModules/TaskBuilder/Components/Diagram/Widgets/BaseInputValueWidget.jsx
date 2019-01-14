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
        let value;
        if (this.props.defaultValue) {
            value = this.props.defaultValue.value;
        }

        switch (this.props.inputType) {
            case "bare":
            case "plain":
                return <div />;
            case "field":
                return (
                    <div {...this.getProps()}>
                        <input
                            type="text"
                            value={value}
                            onChange={e => this.setValue(e)}
                        />
                    </div>
                );
            case "dropdown":
                return (
                    <div {...this.getProps()}>
                        <select onChange={e => this.setValue(e)} defaultValue={value}>
                            {this.props.valueOptions.map((option, i) =>
                                (
                                    <option key={i}>
                                        {option.value}
                                    </option>
                                )
                            )}
                        </select>
                    </div>
                );
        }
    }
}