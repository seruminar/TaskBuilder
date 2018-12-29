class FunctionTrayItem extends React.Component {
    render() {
        const label = this.props.functionModel.displayName || this.props.functionModel.name;
        const functionModelData = JSON.stringify(this.props.functionModel);

        return (
            <div
                draggable className="task-builder-tray-item"
                onDragStart={e => {
                    e.dataTransfer.setData("functionModel", functionModelData);
                }}
            >
                <i className="icon-w-products-data-source" />
                <span>{label}</span>
            </div>
        );
    }
}