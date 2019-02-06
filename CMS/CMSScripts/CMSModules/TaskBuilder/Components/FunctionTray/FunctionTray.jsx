class FunctionTray extends React.Component {
    functions = taskBuilderDataSource.getAuthorizedFunctions();

    render() {
        const collapsedStyle = this.functions.length ? null : { flex: 0 };

        return (
            <div className="task-builder-function-tray" style={collapsedStyle}>
                {this.functions.map((f, index) =>
                    <FunctionTrayItem functionModel={f} key={index} />
                )}
            </div>
        );
    }
}