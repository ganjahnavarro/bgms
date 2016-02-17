Public Interface IControl

    Sub loadObject()

    Sub saveObject()

    Sub updateObject()

    Sub deleteObject()

    Sub deleteRow()

    Sub reset()

    Sub search()

    Sub nextObject()

    Sub previousObject()

    Sub firstObject()

    Sub lastObject()

    Sub printObject()

    Sub resetListSelection()

    Sub enableInputs(ByVal enable As Boolean)

    Function getButtonsShown() As Integer

    Function getErrorMessage() As String

    Function getAllowEditDelete() As Boolean

End Interface
