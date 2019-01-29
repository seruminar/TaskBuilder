class FunctionTrayItem extends React.Component {
    render() {
        const typeGuid = this.props.functionModel.typeGuid;

        return (
            <div
                draggable className="task-builder-tray-item"
                onDragStart={e => {
                    e.dataTransfer.setData("typeGuid", typeGuid);
                }}
            >
                <i className="icon-w-products-data-source" />
                <span>{this.props.functionModel.displayName}</span>
            </div>
        );
    }
}