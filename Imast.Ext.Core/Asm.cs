using System.IO;
using System.Text;

namespace Imast.Ext.Core
{
    /// <summary>
    /// Model to work with assemblies
    /// </summary>
    public static class Asm
    {
        /// <summary>
        /// Get the file content from assembly
        /// </summary>
        /// <param name="assembly">The assembly to load</param>
        /// <param name="file">The file to get content from</param>
        /// <returns></returns>
        public static string GetEmbeddedResource(string assembly, string file)
        {
            // load assembly
            var assemblyInstance = System.Reflection.Assembly.Load(assembly);

            // get resource stream and data
            var templateStream = assemblyInstance.GetManifestResourceStream(file);

            // make sure not null
            if (templateStream == null)
            {
                throw new FileLoadException("Error while loading the file");
            }

            var data = new BinaryReader(templateStream).ReadBytes((int)templateStream.Length);

            // close the stream
            templateStream.Close();

            return Encoding.UTF8.GetString(data);
        }
    }
}