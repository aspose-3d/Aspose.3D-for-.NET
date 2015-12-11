'///////////////////////////////////////////////////////////////////////
' Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
'
' This file is part of Aspose.3D. The source code in this file
' is only intended as a supplement to the documentation, and is provided
' "as is", without warranty of any kind, either expressed or implied.
'///////////////////////////////////////////////////////////////////////
Imports System.IO
Namespace License
    Public Class ApplyLicense
        Public Shared Sub UsingFile()
            ' ExStart:ApplyLicenseUsingFile
            Dim license As New Aspose.ThreeD.License()
            license.SetLicense("Aspose.3D.lic")
            ' ExEnd:ApplyLicenseUsingFile
        End Sub
        Public Shared Sub UsingStreamObject()
            ' ExStart:ApplyLicenseUsingStreamObject
            Dim license As New Aspose.ThreeD.License()
            Dim myStream As New FileStream("Aspose.3D.lic", FileMode.Open)
            license.SetLicense(myStream)
            ' ExEnd:ApplyLicenseUsingStreamObject
        End Sub
        Public Shared Sub UsingEmbeddedResource()
            ' ExStart:ApplyLicenseUsingEmbeddedResource
            ' Instantiate the License class
            Dim license As New Aspose.ThreeD.License()

            ' Pass only the name of the license file embedded in the assembly
            license.SetLicense("Aspose.3D.lic")
            ' ExEnd:ApplyLicenseUsingEmbeddedResource
        End Sub
    End Class
End Namespace
