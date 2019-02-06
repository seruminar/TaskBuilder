class TaskBuilderDataSource {
    models;
    graph;
    endpoints;
    secureToken;

    constructor(props) {
        this.models = props.models;
        this.graph = props.graph;
        this.endpoints = props.endpoints;
        this.secureToken = props.secureToken;

        if (!this.graph.graph.inputValues) {
            this.graph.graph.inputValues = {};
        }
    }

    getAuthorizedFunctions() {
        return _.intersectionWith(
            this.models.functions,
            this.models.authorizedFunctionGuids,
            (a, b) => a.typeGuid === b
        );
    }

    getFunctionByTypeGuid(typeGuid) {
        return _.find(this.models.functions, f => f.typeGuid === typeGuid);
    }

    getInputValue(inputID) {
        return this.graph.graph.inputValues[inputID];
    }

    setInputValue(inputID, inputValue) {
        this.graph.graph.inputValues[inputID] = JSON.parse(JSON.stringify(inputValue));
    }

    removeInputValue(inputID) {
        delete this.graph.graph.inputValues[inputID];
    }
}