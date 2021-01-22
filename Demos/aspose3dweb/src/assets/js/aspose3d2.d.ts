
declare namespace Aspose3D {
    export type Vector2 = [number, number];
    export type Vector3 = [number, number, number];
    export type Matrix4 = [number, number, number, number,number, number, number, number,number, number, number, number,number, number, number, number];

    export type BoundingBox = {
        minimum : Vector3;
        maximum : Vector3;
        isNull() : boolean;
    }

    export enum Axis { X , Y , Z }
    export enum CompareFunction {
        Always,
        Never,
        Equal,
        NotEqual,
        Greater,
        GEqual,
        Less,
        LEqual
    }

    /**
     * The meta data of the scene
     */
    export class SceneMetaData {
        private constructor();
        public get fileName() : string;
        public get comment() : string;
        public get creationTime() : string;
        public get author() : string;
        public get authoringTool() : string;
        public get format() : string;
        public get keywords() : string;
        public get revision() : string;
        public get subject() : string;
        public get title() : string;
        public get unitName() : string;
        public get unitScaleFactor() : number;
        public get originalUp() : Axis;
        public get up() : Axis;
    }
    /**
     * Scene instance
     */
    export class Scene {
        private constructor();
        /**
         * Get the root node of the scene
         */
        public get rootNode() : Node;
        public createNode() : Node;
        public getMetaData() : SceneMetaData;
        /**
         * Load scene from array buffer 
         * @param buffer  
         */
        public static fromBuffer(buffer : ArrayBuffer): Scene;
    }
    export class Transform {
        private constructor();
        public getMatrix() : Matrix4;
        public clear();
        public translate(x : number, y : number, z : number);
        public scale(x : number, y : number, z : number);
    }
    /**
     * Scene node used to construct scene hierarchy structure
     */
    export class Node {
        private constructor();

        public addChildNode(child : Node);
        public addEntity(entity : Entity);
        public addMaterial(material : Material);

        public clearEntities();
        public createChildNode(entity : Entity);
        public removeEntity(entity : Entity);
        /**
         * Gets the name of the current node
         */
        public get name() : string;
        /**
         * Sets the name of the current node
         */
        public set name(v : string);
        /**
         * Gets the default entity of current node
         */
        public get entity() : Entity;
        /**
         * Sets the default entity of current node
         */
        public set entity(v : Entity);
        /**
         * Gets the transformation in world coordinate system.
         */
        public worldTransform() : Transform;
        /**
         * Gets the transformation in local coordinate system.
         */
        public localTransform() : Transform;
        /**
         * Gets the transformation in geometric coordinate system.
         * This is similar to localTransform but only applied to geometries attached to current node.
         */
        public geometricTransform() : Transform;
        /**
         * Gets the bounding box of current node
         */
        public getBoundingBox() : BoundingBox;
        /**
         * Gets the parent node of current node
         */
        public get parent() : Node;
        /**
         * Gets the scene instance that this node belongs to
         */
        public get scene() : Scene;
        /**
         * Gets the children nodes
         */
        public get children() : Node[];
    }

    interface RenderStateMap {
        "cullFace" : boolean;
        "depthTest" : boolean;
        "depthWrite" : boolean;
        "depthFunction" : CompareFunction;
        "blend" : boolean;
    }
    /**
     * Base class of all materials
     */
    export class Material {
        protected constructor();
        public setRenderState<T extends keyof RenderStateMap>(rs : T, value : RenderStateMap[T]);
    }
    /**
     * Blinn-phong shading model
     */
    export class PhongMaterial extends Material {
        protected constructor();
        public get diffuse() : Vector3;
        public set diffuse(v : Vector3);
    }
    /**
     * Physical-based rendering model.
     */
    export class PbrMaterial extends Material {
        protected constructor();
        public get albedo() : Vector3;
        public set albedo(v : Vector3);
    }
    export class TextureUnit {
        protected constructor();
    }
    export class RenderWindow {
        /**
         * Construct a render window from the canva
         * @param canvasId 
         */
        public constructor(canvasId : String);

        /**
         * Start the render loop, then the canvas will automatically update its content without manually render 
         */
        public startRenderLoop();
    }
    /**
     * The base class of all entities that can be attached to the node.
     */
    export class Entity {
        protected constructor();
        public get scene() : Scene;        
        public get parent() : Node;        
    }
    export enum ProjectionType 
    {
        Ortho,
        Perspective
    }
    export class Frustum extends Entity {
        public get projectionType() : ProjectionType;
        public set projectionType(v : ProjectionType);
        public get up() : Vector3;
        public set up(v : Vector3);
        public get direction() : Vector3;
        public set direction(v : Vector3);
        public get fov() : number;
        public set fov(v : number);
        public get near() : number;
        public set near(v : number);
        public get far() : number;
        public set far(v : number);
        public lookAt(target : Vector3) : void;
    }
    export class Camera extends Frustum {
        public constructor();
    }
    /**
     * Light 
     */
    export class Light extends Frustum {
        public constructor();
        /**
         * Gets the light type
         */
        public get type() : LightType;
        /**
         * Sets the light type
         */
        public set type(type : LightType);
        /**
         * Gets the color of the light
         */
        public get color() : Vector3;
        /**
         * Sets the color of the light
         */
        public set color(v : Vector3);
    }
    /**
     * Light's type
     */
    export enum LightType
    {
        /**
         * The light is a directional light, it's position will not affect the shading.
         */
        Directional,
        /**
         * The light is a omni light source, it's direction will not affect the shading.
         */
        Omni,
        /**
         * The light is a spot light, it's position and direction will affect the shading.
         */
        Spot
    }
    export class Mesh extends Entity {

    }
    /**
     * This entity encapsulates a set of lines
     */
    export class LineSet extends Entity {
        public constructor();
        /**
         * Clear all lines
         */
        public clear();
        /**
         * Append a new line to this line set instance. 
         * @param from 
         * @param to 
         */
        public line(from : Vector3, to : Vector3);
        /**
         * Clear all lines and create lines based on a set of continous control points 
         * @param points 
         */
        public lineStrip(points : Vector3[]);
    }

    /**
     * Manages the command to extend the renderer
     */
    export class CommandManager
    {
        protected constructor();
        /**
         * Register a top-level command
         * @param cmd The command to register
         */
        public registerCommand(cmd : Command);
        /**
         * Register a sub command under specified parent command 
         * @param parent The name of the parent command
         * @param cmd The command to register
         */
        public registerCommandEx(parent : string, cmd : Command);

    }
    export type CommandDef = {
        /**
         * Name of the command
         */
        name : string;
        /**
         * Display name of the command for human reading.
         */
        text : string;
        /**
         * Implementation of this command when it is get called.
         */
        execute : () => void;
        /**
         * Called when the command was registered to the command manager.
         */
        registered? : () => void;
        /**
         * Return the checked status of this command.
         */
        checked? : () => boolean;
    }
    /**
     * The command implementation
     */
    export class Command {
        public constructor(def : CommandDef);
        /**
         * Manually execute this command
         */
        public execute() : void;
        /**
         * Return true if this command is checked.
         */
        public checked() : boolean;

        /**
         * Gets the name of this command
         */
        public get name() : string;
        /**
         * Gets the text of this command that may used in the menu entry.
         */
        public get text() : string;
        /**
         * Sets the text of this command that may used in the menu entry.
         */
        public set text(v : string);

    }

    export type EventType = "keydown" | "keyup" | "keypress" | "mousedown" | "mouseup" | "mousemove" | "wheel" | "sceneChanged";
    interface RendererEventMap {
        "keydown" : KeyboardEvent;
        "keyup" : KeyboardEvent;
        "keypress" : KeyboardEvent;
        "mousedown" : MouseEvent;
        "mouseup" : MouseEvent;
        "mousemove" : MouseEvent;
        "wheel" : WheelEvent;
        "sceneChanged" : Event;
    }

    export type RendererFeature =
        //Enable the main menu
        | "menu"
        //Highlight the selected object
        | "selection"
        //Enable the BIM viewer
        | "bim"
        //Enable the property grid
        | "property-grid"
        //Enable the reference grid
        | "grid"
        //Enable the summary information panel
        | "summary"
        ;


    export enum ShadingModel
    {
        /**
         * Blinn-phong shading model
         */
        Phong,
        /**
         * Lambert shading model
         */
        Lambert,
        /**
         * The physical-based rendering material.
         */
        Pbr
    }
    export class Renderer {
        /**
         * Construct a new renderer based on specified render window
         * @param renderWindow Which render window will be rendered to.
         */
        public constructor(renderWindow : RenderWindow);
        /**
         * Create a new material instance.
         * @param shadingModel Material's shading model
         */
        public createMaterial(shadingModel : ShadingModel) : Material;

        /**
         * Gets current camera
         */
        public get camera() : Camera;
        /**
         * Sets current camera
         */
        public set camera(v : Camera);
        /**
         * Gets current scene
         */
        public get scene() : Scene;
        /**
         * Sets current scene
         */
        public set scene(v : Scene);

        /**
         * Gets current movement
         */
        public get movement() : Movement;

        /**
         * Sets current movement
         */
        public set movement(v : Movement);

        /**
         * Gets current render window
         */
        public get window() : RenderWindow;

        /**
         * Gets the time elapsed since the renderer was created.
         */
        public time() : number;

        /**
         * Invalidates the render window to request for a repaint.
         */
        public invalidate() : void;

        /**
         * Pick the 3D world position from given 2D screen position
         * @param x The x component of the screen position ranged in [0, 1]
         * @param y The y component of the screen position ranged in [0, 1]
         */
        public pick(x : number, y : number) : Vector3 | null;

        /**
         * Register the widget controller to this renderer.
         * @param controller The widget controller to register
         */
        public registerUI(controller : WidgetController);

        /**
         * Add a event listener to specified renderer's internal event
         * @param event The event to register
         * @param listener The event handler
         */
        public addEventListener<K extends keyof RendererEventMap>(event : K, listener : (this: Renderer, e : RendererEventMap[K]) => void);

        /**
         * Enable a renderer feature
         * @param feature The feature to enable
         */
        public enableFeature(feature : RendererFeature); 

        /**
         * Gets the command manager
         */
        public getCommandManager() : CommandManager;
        /**
         * Dispatch a event to renderer's internal component
         */
        public dispatchEvent(e : object) : void
    }


    /**
     * The movement defines how camera will be interacted with user
     */
    export class Movement
    {

    }
    /**
     * The camera moves in a sphere orbital.
     */
    export class OrbitalMovement extends Movement
    {
        public constructor();
        /**
         * Gets the center of the orbit
         */
        public get center() : Vector3;
        /**
         * Sets the center of the orbit
         */
        public set center(v : Vector3);

        /**
         * The maximum distance to the center
         */
        public get maximumDistance() : number;
        /**
         * The maximum distance to the center
         */
        public set maximumDistance(v : number);

        /**
         * The minimum distance to the center
         */
        public get minimumDistance() : number;
        /**
         * The minimum distance to the center
         */
        public set minimumDistance(v : number);
    }

    /**
     * The base class of all kinds of curves
     */
    export class Curve
    {
        protected constructor();
    }
    export class Line extends Curve
    {
        public constructor();
        /**
         * Construct a line curve from two points
         * @param from 
         * @param to 
         */
        public static fromPoints(from : Vector3, to : Vector3) : Line;

        /**
         * Gets the first point of the line
         */
        public get p0() : Vector3;
        /**
         * Sets the first point of the line
         */
        public set p0(v : Vector3);
        /**
         * Gets the second point of the line
         */
        public get p1() : Vector3;
        /**
         * Sets the second point of the line
         */
        public set p1(v : Vector3);
    }

    export class Ellipse extends Curve
    {
        public constructor()
        public get semiAxis1() : number;
        public set semiAxis1(v : number);
        public get semiAxis2() : number;
        public set semiAxis2(v : number);
    }
    export class Circle extends Curve
    {
        public constructor();
        public static create(radius : number) : Circle;
        public get radius() : number;
        public set radius(v : number);
    }
    /**
     * This class encapsulates the end point of a curve without knowing concrete representation. 
     */
    export class EndPoint
    {
        protected constructor();
        public setDegree(v : number);
        public setRadian(v : number);
        public setPoint(v : Vector3);
    }
    /**
     * Trimmed curve with specified start and final end point. 
     */
    export class TrimmedCurve extends Curve
    {
        public constructor();
        public get basisCurve() : Curve;
        public set basisCurve(v : Curve);
        public getFirst() : EndPoint;
        public getSecond() : EndPoint;
    }
    export class TransformedCurve extends Curve
    {
        public constructor();
        public static create(curve : Curve, transform : Matrix4) : TransformedCurve;
        public get basisCurve() : Curve;
        public set basisCurve(v : Curve);

        public get transformMatrix() : Matrix4;
        public set transformMatrix(v : Matrix4);
    }
    export class CompositeCurve extends Curve
    {
        public constructor();
        public addSegment(curve : Curve, sameDirection : boolean);
    }


    export type ModelEntryDef = {
        type : 'int' | 'float';
        value : number
    } | {
        type : 'string';
        value : string
    } | {
        type : 'bool';
        value : boolean;
    } | {
        type : 'vec2';
        value : Vector2;
    } | {
        type : 'vec3';
        value : Vector3;
    };

    export type ModelDef = {[T : string] : ModelEntryDef};

    export type WidgetDef = {
        type : 'ComboBox';
        id? : string;
        items : Array<string>
    } | {
        type : 'Button',
        id? : string,
        text? : string,
    } | {
        type : 'Text';
        id? : string;
        text? : string;
    } | {
        type : 'Window';
        pos? : Vector2;
        size? : Vector2;
        id? : string;
        text? : string;
        children? : Array<WidgetDef>;
    }

    export type ActionDef = {
        [id : string] : (controller : WidgetController) => void
    }

    export class WidgetController {
        public constructor(model : ModelDef, widget : WidgetDef, actions : ActionDef)
        public get(modelEntry : string) : any;
        public set(modelEntry : string, value : any);
        /**
         * Sets the property of the widget by it's id 
         * @param widgetId The widget's id to set
         * @param prop The widget's property name to set
         * @param value The value to assign to the widget's property
         */
        public setWidgetProperty(widgetId : string, prop : string, value : any);
    }
}