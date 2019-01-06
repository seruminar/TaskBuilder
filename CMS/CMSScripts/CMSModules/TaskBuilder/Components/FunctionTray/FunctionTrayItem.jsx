class FunctionTrayItem extends React.Component {
    render() {
        return (
            <div
                draggable className="task-builder-tray-item"
                onDragStart={e => {
                    e.dataTransfer.setData("functionModel", this.props.functionModel.type);
                }}
            >
                <i className="icon-w-products-data-source" />
                <span>{this.props.functionModel.displayName}</span>
            </div>
        );
    }
}