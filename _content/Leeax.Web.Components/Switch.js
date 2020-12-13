export function move(wrapper, item, index) {
    if (!wrapper.children || wrapper.children.length <= index || !wrapper || !item) {
        return false;
    }
    const targetEl = wrapper.children[index];
    const targetBCR = targetEl.getBoundingClientRect();
    const indicatorPos = targetBCR.left - wrapper.getBoundingClientRect().left;
    item.style.left = indicatorPos + "px";
    item.style.width = "calc(" + targetBCR.width + "px - 0.4em)";
    return true;
}
