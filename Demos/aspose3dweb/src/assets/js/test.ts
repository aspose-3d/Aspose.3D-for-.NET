
function test() {
    var a : Aspose3D.ModelDef = {
        'ff' : {type:'string', value:'ff'},
    };
    var w : Aspose3D.WidgetDef = {
        type : 'Window',
        text : 'Hello', 
        children : [
            {
                'type' : 'Button',
                'id' : 'f',
                'text' : 'Hello'
            }
        ]
    };
    var acts : Aspose3D.ActionDef = {
        'act' : function(wc : Aspose3D.WidgetController) {
        }
    }
}