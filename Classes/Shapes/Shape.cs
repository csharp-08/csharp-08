using csharp_08.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    /// <summary>
    /// Class that stores a Shape object.
    /// </summary>
    public abstract class Shape
    {
        private static uint IDs = 1;

        // Shape ID. Needs to be public to retrieve shape from the database on reboot.
        public uint ID { get; set; }

        public List<Tuple<double, double>> Vertices { get; private set; }
        public ShapeConfig Config { get; private set; }
        public User Owner { get; set; }
        public byte OverrideUserPolicy { get; set; }
        public ShapeCode Code;

        /// <summary>
        /// Constructor for the Shape class.
        /// </summary>
        /// <param name="Vertices">The coordinates of the points that describe the shape</param>
        /// <param name="Owner">The user owning the shape</param>
        /// <param name="Config">The graphical configuration of the shape</param>
        /// <param name="ID">The unique id of the shape</param>
        protected Shape(List<Tuple<double, double>> Vertices, User Owner = null, ShapeConfig Config = null, uint ID = 0)
        {
            if (ID == 0)
            {
                this.ID = IDs;
                IDs++;
            }
            else
            {
                this.ID = ID;
            }

            this.Vertices = Vertices;
            this.Owner = Owner;
            if (Config == null)
            {
                this.Config = ShapeConfig.DefaultConfig;
            }
            else
            {
                this.Config = Config;
            }

            // Default values for shape code and policy.
            this.Code = ShapeCode.Pencil;
            this.OverrideUserPolicy = 0;
        }

        /// <summary>
        /// Function that returns the code id of the Shape object.
        /// </summary>
        /// <returns>The code id of the shape object</returns>
        public abstract byte GetShapeCode();

        /// <summary>
        /// Function that returns a string describing the shape in the SVG format.
        /// </summary>
        /// <returns>String describing the shape in the SVG format</returns>
        public abstract string ToSVG();

        /// <summary>
        /// Fonction that serialize the shape into a JSON
        /// </summary>
        /// <returns></returns>
        public string Draw()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Updates the parameters of the shape (after a modification of this
        /// shape by the user on the interactive board for exemple).
        /// </summary>
        /// <param name="updatedShape">A Shape object that contains the updated version of the current shape</param>
        public void UpdateWithNewShape(Shape updatedShape)
        {
            if (this.ID != updatedShape.ID)
            {
                return;
            }

            this.Vertices = updatedShape.Vertices;
            this.Config = updatedShape.Config;
        }

        /// <summary>
        /// Update the value of the next id to be given to a newly created shape.
        /// </summary>
        /// <param name="StartPoint">Value of the next id to be given to a newly created shape</param>
        public static void UpdateStartPoint(uint StartPoint)
        {
            IDs = StartPoint > IDs ? StartPoint : IDs;
        }

        /// <summary>
        /// ToString function which describes the object with a string
        /// </summary>
        /// <returns>String describing the object</returns>
        public override string ToString()
        {
            string str = "";

            str += "ID: " + ID;
            str += Environment.NewLine;
            str += "Propriétaire: " + Owner.Username;
            str += Environment.NewLine;
            str += "Liste des points:";
            foreach (Tuple<double, double> point in Vertices)
            {
                str += Environment.NewLine;
                str += "\tx: " + point.Item1 + ", y: " + point.Item2;
            }
            str += Environment.NewLine;
            str += "Config: " + Config;

            return str;
        }
    }
}