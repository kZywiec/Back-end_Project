using System;
using System.ComponentModel;

namespace Core.Entities.DocumentEntities
{
    public enum DocumentAccessStatus
    {
        [Description("Any user, including unregistered users, can search for and download the Document.")]
        Public,

        [Description("Only registered users can search for and download the Document.")]
        Private,

        [Description("Only Admin and those who added the Document can search for and download it.")]
        Confidential
    }
}