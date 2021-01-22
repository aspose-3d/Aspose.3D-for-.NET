class Node<K, V>{
    key: K;
    value: V;
}

export class HashMap<K, V> {

    private nodes: Node<K, V>[] = [];

    public put(key: K, value: V) {
        let node: Node<K, V> = this.getNodeByKey(key);
        if (null == node) {
            node = new Node<K, V>();
            node.key = key;
            node.value = value;
            this.nodes[this.nodes.length] = node;
        } else {
            this.nodes[this.nodes.indexOf(node)].value = value;
        }
    }

    public queryAll() {
        return this.nodes;
    };

    public containKey(key: K): boolean {
        let node = this.getNodeByKey(key);
        if (null == node) {
            return false;
        }
        return true;
    };

    public getKey(value: V) {
        let node = this.getNodeByValue(value);
        return node.key;
    }

    public getValue(key: K) {
        let node = this.getNodeByKey(key);
        return node.value;
    }

    private getNodeByKey(key: K): Node<K, V> {
        let currentNodes = this.nodes;
        for (let i = 0; i < currentNodes.length; i++) {
            if (currentNodes[i].key == key) {
                return currentNodes[i];
            }
        }
        return null;
    };

    private getNodeByValue(value: V): Node<K, V> {
        let currentNodes = this.nodes;
        for (let i = 0; i < currentNodes.length; i++) {
            if (currentNodes[i].value == value) {
                return currentNodes[i];
            }
        }
        return null;
    };

    public remove(key: K) {
        let node = this.getNodeByKey(key);
        if (null != node) {
            this.nodes.splice(this.nodes.indexOf(node), 1);
        }
    }

}