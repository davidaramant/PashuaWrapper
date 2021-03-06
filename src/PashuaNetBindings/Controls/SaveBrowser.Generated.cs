using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pashua
{
    /// <summary>
    /// A savebrowser can be used for setting a path and name for a new file.
    /// </summary>
    /// <remarks>
    /// It consists of a textfield, a button and (optionally) a label. The textfield holds the actual element value
    /// (the file path and name), while the button (which is localized) is used to invoke a file selector sheet.
    /// </remarks>
    public sealed partial class SaveBrowser : IPashuaControl
    {
        /// <summary>
        /// The name of this element in the Pashua script.  Should not be needed outside of the framework.
        /// </summary>
        public string Id => "savebrowser" + GetHashCode();

        /// <summary>
        /// Creates a label above this element
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Sets the width (overall width of texfield and button)
        /// </summary>
        public int Width { get; set; } = 300;

        /// <summary>
        /// Default path
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// File extension to use for the save dialog box; if this attribute is used, the user will be forced to use
        /// that extension for the name
        /// </summary>
        public string Filetype { get; set; }

        /// <summary>
        /// If set to true, input is mandatory.
        /// </summary>
        public bool Mandatory { get; set; } = false;

        /// <summary>
        /// If present, this string will be as the field’s placeholder string.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Absolute horizontal position in the window, measured from the left border of the content area
        /// </summary>
        public int? X { get; set; }

        /// <summary>
        /// Absolute vertical position in the window, measured from the lower border of the content area
        /// </summary>
        public int? Y { get; set; }

        /// <summary>
        /// Horizontal offset, relative to the position the element would have if RelX was not used (e.g.: RelX
        /// specifies the distance from the left window border). Any integer can be used as RelX value.
        /// </summary>
        public int RelX { get; set; } = 0;

        /// <summary>
        /// Relative vertical distance to the next element below (“relative” means that the value is added to the
        /// default distance). Any integer larger than -20 can be used as RelY value.
        /// </summary>
        public int RelY { get; set; } = 0;

        /// <summary>
        /// Writes the control script to the given writer.
        /// </summary>
        /// <exception cref="PashuaScriptException">Thrown if the control was not configured correctly.</exception>
        public void WriteTo(TextWriter writer)
        {
            var errors = GetValidationIssues();
            if(errors.Any())
            {
                throw new PashuaScriptException(errors);
            }

            writer.WriteLine($"{Id}.type = savebrowser");
            WriteSpecialProperties(writer);
            if (!string.IsNullOrWhiteSpace(Label))
            {
                writer.WriteLine($"{Id}.label = {Label}");
            }
            if (Width != 300)
            {
                writer.WriteLine($"{Id}.width = {Width}");
            }
            if (!string.IsNullOrWhiteSpace(Default))
            {
                writer.WriteLine($"{Id}.default = {Default}");
            }
            if (!string.IsNullOrWhiteSpace(Filetype))
            {
                writer.WriteLine($"{Id}.filetype = {Filetype}");
            }
            if (Mandatory != false)
            {
                writer.WriteLine($"{Id}.mandatory = {(Mandatory ? 1 : 0)}");
            }
            if (!string.IsNullOrWhiteSpace(Placeholder))
            {
                writer.WriteLine($"{Id}.placeholder = {Placeholder}");
            }
            if (X != null)
            {
                writer.WriteLine($"{Id}.x = {X}");
            }
            if (Y != null)
            {
                writer.WriteLine($"{Id}.y = {Y}");
            }
            if (RelX != 0)
            {
                writer.WriteLine($"{Id}.relx = {RelX}");
            }
            if (RelY != 0)
            {
                writer.WriteLine($"{Id}.rely = {RelY}");
            }
        }

        partial void WriteSpecialProperties(TextWriter writer);

        /// <summary>
        /// Returns all the validation errors with the control.
        /// </summary>
        /// <returns>All the issues.</returns>
        public IEnumerable<string> GetValidationIssues()
        {
            var errors = new List<string>();
            if (X < 0)
            {
                errors.Add("SaveBrowser X must be greater than or equal to 0.");
            }
            if (Y < 0)
            {
                errors.Add("SaveBrowser Y must be greater than or equal to 0.");
            }
            if (Width < 0)
            {
                errors.Add("SaveBrowser Width must be greater than or equal to 0.");
            }
            if (RelY <= -20)
            {
                errors.Add("SaveBrowser RelY must be greater than -20.");
            }
            AdditionalValidation(errors);
            return errors;
        }

        partial void AdditionalValidation(List<string> errors);
    }
}
