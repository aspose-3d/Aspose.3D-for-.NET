

type UpAxis = 'x' | 'y' | 'z';

type CameraSettings = {
    up : UpAxis,
    /**
     * Far plane distance, default value is 100
     */
    far? : number;
    /**
     * Near plane distance, default value is 0.1
     */
    near? : number;
    /**
     * Field of view, in degree, default value is 45.
     */
    fov? : number;
    position? : number[],
    lookAt? : number[],
};

type AsposeInit = {
    /**
     * The id of the canvas that will be used by the 3D renderer.
     */
    canvas : string;
    /**
     * The A3DW file to be imported.
     */
    url? : string;
    /**
     * Initial camera settings
     */
    camera? : CameraSettings;

    features? : string[];

    /**
     * Center the model to the grid plane, default value is false
     */
    centerModel? : boolean;

    /**
     * The camera's movement style, default value is 'standard'
     */
    movement? : 'standard' | 'orbital';
    /**
     * Show a grid, default value is true
     */
    grid? : boolean;
    /**
     * Show ruler of the scene, default value is false.
     */
    ruler? : boolean;
    /**
     * Enable UI or not, default value is true
     */
    ui? : boolean;
    /**
     * Enable orientation box, default value is true
     */
    orientationBox? : boolean;
    /**
     * The callback when the scene from specified url has been loaded. 
     */
    onload? : (root : any) => void;
};

interface Window {
    aspose3d(init : AsposeInit) : void
}
