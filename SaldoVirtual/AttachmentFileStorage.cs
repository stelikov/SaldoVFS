namespace SaldoVirtual
{
    public interface IAttachmentFileStorage
    {
        void SaveAttachmentFile(string old, string fileName);
    }

    public class AttachmentFileStorage : IAttachmentFileStorage
    {
        public void SaveAttachmentFile(string old, string fileName)
        {
            System.IO.File.Copy(old, fileName);
        }
    }
}
