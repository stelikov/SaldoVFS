namespace SaldoVirtual
{
    public interface IAttachmentFileStorage
    {
        void SaveAttachmentFile(string old, string fileName);

        void RemoveAttachmentFile(string fileName);
    }

    public class AttachmentFileStorage : IAttachmentFileStorage
    {
        public void SaveAttachmentFile(string old, string fileName)
        {
            System.IO.File.Copy(old, fileName);
        }

        public void RemoveAttachmentFile(string fileName)
        {
            System.IO.File.Delete(fileName);
        }
    }
}
