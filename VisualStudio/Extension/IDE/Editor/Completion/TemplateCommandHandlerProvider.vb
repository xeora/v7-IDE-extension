﻿Imports System.ComponentModel.Composition
Imports Microsoft.VisualStudio.Editor
Imports Microsoft.VisualStudio.Language.Intellisense
Imports Microsoft.VisualStudio.OLE.Interop
Imports Microsoft.VisualStudio.Shell
Imports Microsoft.VisualStudio.Text.Editor
Imports Microsoft.VisualStudio.Text.Operations
Imports Microsoft.VisualStudio.TextManager.Interop
Imports Microsoft.VisualStudio.Utilities

Namespace Xeora.Extension.VisualStudio.IDE.Editor.Completion
    <Export(GetType(IVsTextViewCreationListener))>
    <ContentType(EditorExtension.TemplateContentType)>
    <TextViewRole(PredefinedTextViewRoles.Editable)>
    Public NotInheritable Class TemplateCommandHandlerProvider
        Implements IVsTextViewCreationListener

        <Import()>
        Public Property AdapterService() As IVsEditorAdaptersFactoryService

        <Import()>
        Public Property CompletionBroker() As ICompletionBroker

        <Import()>
        Public Property ServiceProvider() As SVsServiceProvider

        <Import>
        Public Property ContentTypeRegistryService As IContentTypeRegistryService

        Public Sub VsTextViewCreated(ByVal textViewAdapter As IVsTextView) Implements IVsTextViewCreationListener.VsTextViewCreated
            Dim textView As IWpfTextView = AdapterService.GetWpfTextView(textViewAdapter)
            If textView Is Nothing Then Return

            textView.Properties.GetOrCreateSingletonProperty(Of TemplateCommandHandler)(
                Function() New TemplateCommandHandler(textViewAdapter, textView, Me))
        End Sub
    End Class
End Namespace