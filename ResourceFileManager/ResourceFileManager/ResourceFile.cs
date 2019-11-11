using System;
using ResourceFileManager.ResourceFileOperators;

namespace ResourceFileManager
{
    public class ResourceFile : IResourceFile
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }

        private string _fullPath;
        public string FullPath
        {
            get
            {
                return _fullPath;
            }
            set
            {
                _fullPath = value;
                DefinePathRelatedFields(value);
            }
        }

        public object Content { get; set; }
        public Type ContentType { get; set; }

        public IResourceFileOperator ResourceFileOperator { get; set; }

        public ResourceFile(string fullPath)
        {
            FullPath = fullPath;
            DefinePathRelatedFields(fullPath);
        }

        public ResourceFile()
        {
        }

        public bool Load()
        {
            try
            {
                Content = ResourceFileOperator?.Read(FullPath, ContentType);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool LoadFrom(string fullPath)
        {
            DefinePathRelatedFields(fullPath);

            try
            {
                Content = ResourceFileOperator?.Read(fullPath, ContentType);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool Save()
        {
            bool isWritingSuccessful = false;

            try
            {
                isWritingSuccessful = ResourceFileOperator.Write(FullPath, Content, ContentType);
            }
            catch (Exception e) when (e is NullReferenceException)
            {
                Console.WriteLine(e.ToString());
            }

            return isWritingSuccessful;
        }

        public bool SaveAs(string fullPath)
        {
            DefinePathRelatedFields(fullPath);
            bool isWritingSuccessful = false;

            try
            {
                isWritingSuccessful = ResourceFileOperator.Write(fullPath, Content, ContentType);
            }
            catch (Exception e) when (e is NullReferenceException)
            {
                Console.WriteLine(e.ToString());
            }

            return isWritingSuccessful;
        }

        public string GetFullPath()
        {
            return GetFullPathWithExceptionHandling();
        }

        public string GetPathWithoutExtension()
        {
            return GetFullPathWithExceptionHandling();
        }

        protected void DefinePathRelatedFields(string fullPath)
        {
            Path = string.Empty;
            Name = string.Empty;
            Extension = string.Empty;

            try
            {
                Path = System.IO.Path.GetDirectoryName(fullPath);
                Name = System.IO.Path.GetFileNameWithoutExtension(fullPath);
                Extension = System.IO.Path.GetExtension(fullPath);
            }
            catch (Exception e) when (e is ArgumentException || e is ArgumentNullException)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected string GetFullPathWithExceptionHandling()
        {
            string fullPath = string.Empty;

            try
            {
                fullPath = System.IO.Path.Combine(Path, Name) + Extension;
            }
            catch (Exception e) when (e is ArgumentException || e is ArgumentNullException)
            {
                Console.WriteLine(e.ToString());
            }

            return fullPath;
        }
    }
}
