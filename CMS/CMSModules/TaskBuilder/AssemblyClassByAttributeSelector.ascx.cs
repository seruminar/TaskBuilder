using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Base;
using CMS.Base.Web.UI;
using CMS.Core;
using CMS.FormEngine.Web.UI;
using CMS.Helpers;
using CMS.MacroEngine;

public partial class CMSModules_TaskBuilder_AssemblyClassByAttributeSelector : FormEngineUserControl, ICallbackEventHandler
{
    #region "Private variables"

    private readonly string customClassString = ResHelper.GetString("general.customclasses");
    private readonly string noneValue = ResHelper.GetString("general.empty");
    private ClassTypeSettings mSettings;
    private bool mAllowEmpty;

    private static readonly StringSafeDictionary<List<string>> assemblyClasses = new StringSafeDictionary<List<string>>();

    #endregion "Private variables"

    #region "Properties"

    /// <summary>
    /// Gets or sets value for form engine
    /// </summary>
    public override object Value
    {
        get
        {
            return AssemblyName;
        }
        set
        {
            // Set assembly name
            AssemblyName = ValidationHelper.GetString(value, noneValue);

            LoadOtherValues();
        }
    }

    /// <summary>
    /// Gets a column name for full class name
    /// </summary>
    public string ClassNameColumnName
    {
        get
        {
            return GetValue("ClassNameColumnName", String.Empty);
        }
        set
        {
            SetValue("ClassNameColumnName", value);
        }
    }

    /// <summary>
    /// Gets or sets file system filter for list assemblies.
    /// </summary>
    public string AssembliesFilter
    {
        get
        {
            return GetValue("AssembliesFilter", "");
        }
        set
        {
            SetValue("AssembliesFilter", value);
        }
    }

    /// <summary>
    /// Gets or sets base class names (separated by semi-colon) to filter classes.
    /// </summary>
    public string BaseClassNames
    {
        get
        {
            return GetValue("BaseClassName", String.Empty);
        }
        set
        {
            SetValue("BaseClassName", value);
            Settings.BaseClassNames = value;
        }
    }

    /// <summary>
    /// Gets or sets base class attribute to filter classes.
    /// </summary>
    public string BaseClassAttribute
    {
        get
        {
            return GetValue("BaseClassAttribute", String.Empty);
        }
        set
        {
            SetValue("BaseClassAttribute", value);
        }
    }

    /// <summary>
    /// Gets or sets assembly name value to textbox. If 'custom class' is selected gets/sets App_Code.
    /// </summary>
    public string AssemblyName
    {
        get
        {
            string selectedValue = drpAssemblyName.SelectedValue;
            if (AllowEmpty && (selectedValue == noneValue))
            {
                return String.Empty;
            }

            return GetAssemblyName(selectedValue);
        }
        set
        {
            if (AllowEmpty && String.IsNullOrEmpty(value))
            {
                drpAssemblyName.SelectedValue = noneValue;
            }
            else
            {
                drpAssemblyName.SelectedValue = (value == ClassHelper.ASSEMBLY_APPCODE) ? customClassString : value;
            }
        }
    }

    /// <summary>
    /// Gets or sets full class name value to textbox.
    /// </summary>
    public string ClassName
    {
        get
        {
            return drpClassName.SelectedValue;
        }
        set
        {
            drpClassName.SelectedValue = value;
        }
    }

    /// <summary>
    /// Indicates if classes should be selected.
    /// </summary>
    public bool ShowClasses
    {
        get
        {
            return GetValue("ShowClasses", true);
        }
        set
        {
            SetValue("ShowClasses", value);
            Settings.ShowClasses = value;
        }
    }

    /// <summary>
    /// Indicates if enumerations should be selected.
    /// </summary>
    public bool ShowEnumerations
    {
        get
        {
            return GetValue("ShowEnumerations", false);
        }
        set
        {
            SetValue("ShowEnumerations", value);
            Settings.ShowEnumerations = value;
        }
    }

    /// <summary>
    /// Indicates if interfaces should be selected.
    /// </summary>
    public bool ShowInterfaces
    {
        get
        {
            return GetValue("ShowInterfaces", false);
        }
        set
        {
            SetValue("ShowInterfaces", value);
            Settings.ShowInterfaces = value;
        }
    }

    /// <summary>
    /// If true, the control will try to load the assembly and the class to validate it's availability. If false, this check is not done in Validate method.
    /// </summary>
    public bool ValidateAssembly
    {
        get
        {
            return GetValue("ValidateAssembly", true);
        }
        set
        {
            SetValue("ValidateAssembly", value);
        }
    }

    /// <summary>
    /// Indicates if empty value is allowed. Combines its value with FieldInfo.AllowEmpty
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return mAllowEmpty || ((FieldInfo != null) && FieldInfo.AllowEmpty);
        }
        set
        {
            mAllowEmpty = value;
        }
    }

    /// <summary>
    /// Gets error message.
    /// </summary>
    public override string ErrorMessage
    {
        get
        {
            return GetString("AssemblySelector.BadAssemblyOrClass");
        }
    }

    /// <summary>
    /// Gets current class selection settings.
    /// </summary>
    private ClassTypeSettings Settings
    {
        get
        {
            return mSettings ?? (mSettings = new ClassTypeSettings
            {
                BaseClassNames = BaseClassNames,
                ShowClasses = ShowClasses,
                ShowEnumerations = ShowEnumerations,
                ShowInterfaces = ShowInterfaces,
                CheckAutoCreation = CheckAutoCreation
            });
        }
    }

    /// <summary>
    /// Indicates whether automatic type creation by system should be checked.
    /// </summary>
    /// <seealso cref="ClassTypeSettings.CheckAutoCreation"/>
    public bool CheckAutoCreation
    {
        get
        {
            return GetValue("CheckAutoCreation", false);
        }
        set
        {
            SetValue("CheckAutoCreation", value);
            Settings.CheckAutoCreation = value;
        }
    }

    #endregion "Properties"

    #region "Public methods"

    /// <summary>
    /// Loads the other fields values to the state of the form control
    /// </summary>
    public override void LoadOtherValues()
    {
        string classNameValue = (Form != null) ? ValidationHelper.GetString(Form.Data.GetValue(ClassNameColumnName), String.Empty) : String.Empty;
        ClassName = classNameValue;
    }

    /// <summary>
    /// Returns other values related to this form control.
    /// </summary>
    /// <returns>Returns an array where first dimension is attribute name and the second dimension is its value.</returns>
    public override object[,] GetOtherValues()
    {
        if (!String.IsNullOrEmpty(ClassNameColumnName))
        {
            // Set properties names
            object[,] values = new object[1, 2];
            values[0, 0] = ClassNameColumnName;
            values[0, 1] = drpClassName.SelectedValue;
            return values;
        }
        return null;
    }

    /// <summary>
    /// Checks validity of inputs
    /// </summary>
    public override bool IsValid()
    {
        if (!ValidateAssembly)
        {
            return true;
        }

        // Do not validate if macros are used
        if (MacroProcessor.ContainsMacro(AssemblyName) || MacroProcessor.ContainsMacro(ClassName))
        {
            return true;
        }

        // Check allow empty setting
        if (AllowEmpty && (String.IsNullOrEmpty(AssemblyName) || String.IsNullOrEmpty(ClassName)))
        {
            return true;
        }

        try
        {
            // Check assembly and class
            Type classObject = ClassHelper.GetClassType(AssemblyName, ClassName);
            if (classObject != null)
            {
                // Check base class
                if (String.IsNullOrEmpty(BaseClassNames) || (ClassHelper.IsAllowed(classObject, Settings)))
                {
                    return true;
                }
            }
        }
        catch
        {
        }

        // Set validation error message
        ValidationError = ErrorMessage;

        return false;
    }

    #endregion "Public methods"

    #region "Page events"

    /// <summary>
    /// Page_Load event handler.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing && !RequestHelper.IsCallback())
        {
            SetupControl();
        }
    }

    /// <summary>
    /// Render event handler.
    /// </summary>
    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);

        foreach (ListItem item in drpAssemblyName.DropDownList.Items)
        {
            // Get assembly name
            string assemblyName = GetAssemblyName(item.Value);

            // Get classes
            var classes = GetClassesWithAttribute(assemblyName);
            if (classes != null)
            {
                foreach (string className in classes)
                {
                    // Register all possible values for event validation
                    Page.ClientScript.RegisterForEventValidation(drpClassName.DropDownList.UniqueID, className);
                }
            }
        }
    }

    #endregion "Page events"

    #region "Private methods"

    /// <summary>
    /// Gets Assembly name from given value.
    /// </summary>
    /// <param name="value"></param>
    private string GetAssemblyName(string value)
    {
        // Get assembly name
        string assemblyName = value;
        if ((!AllowEmpty && String.IsNullOrEmpty(assemblyName)) || (assemblyName == customClassString))
        {
            assemblyName = ClassHelper.ASSEMBLY_APPCODE;
        }

        return assemblyName;
    }

    /// <summary>
    /// Ensures Assembly selector drop down.
    /// </summary>
    private void EnsureAssemblyData()
    {
        FillAssemblySelector();
    }

    /// <summary>
    /// Ensures Class selector drop down.
    /// </summary>
    private void EnsureClassData()
    {
        FillClassNameSelector(AssemblyName);
    }

    /// <summary>
    /// Ensures selector data.
    /// </summary>
    private void EnsureData()
    {
        EnsureAssemblyData();
        EnsureClassData();
    }

    /// <summary>
    /// Setups control.
    /// </summary>
    private void SetupControl()
    {
        EnsureData();

        // Register scripts
        ScriptHelper.RegisterJQueryUI(Page);
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat(@"
$cmsj(function() {{
    $cmsj('#{0}_txtCombo').on('autocompleteselect', function(event, ui) {{
        var assemblyName = ui.item.value;
        {1}
    }})

    $cmsj('#{0}_txtCombo').on('autocompletesearch', function(event, ui) {{
        var assemblyName = $cmsj('#{0}_txtCombo').val();
        {1}
    }})

    function fillClassInput(returnedArrayAsJSON) {{
        var returnedArray = JSON.parse(returnedArrayAsJSON);
        $cmsj('#{2}_txtCombo').val(returnedArray[0]);
        $cmsj('#{2}_txtCombo').autocomplete('option', 'source', returnedArray);
    }}
}});
", drpAssemblyName.ClientID, Page.ClientScript.GetCallbackEventReference(this, "assemblyName", "fillClassInput", null), drpClassName.ClientID);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "acsDelayedLoad" + ClientID, ScriptHelper.GetScript(sb.ToString()));
    }

    /// <summary>
    /// Fills assembly selector with data.
    /// </summary>
    private void FillAssemblySelector()
    {
        if (AllowEmpty)
        {
            drpAssemblyName.DropDownList.Items.Add(noneValue);
        }

        var classes = GetClassesWithAttribute(ClassHelper.ASSEMBLY_APPCODE);
        if ((classes != null) && (classes.Count > 0))
        {
            drpAssemblyName.DropDownList.Items.Add(customClassString);
        }

        // Get assembly names filtered by given restriction settings
        var assemblies = GetAssembliesWithClassesWithAttribute();

        // Fill assemblies list
        foreach (string assemblyName in assemblies)
        {
            drpAssemblyName.DropDownList.Items.Add(assemblyName);
        }

        // Include macro value if specified
        if (!String.IsNullOrEmpty(AssemblyName) && MacroProcessor.ContainsMacro(AssemblyName))
        {
            drpAssemblyName.DropDownList.Items.Insert(0, AssemblyName);
        }

        // Clear selected value if drop down is empty
        if (drpAssemblyName.DropDownList.Items.Count == 0)
        {
            drpAssemblyName.SelectedValue = String.Empty;
        }
        else
        {
            // Select first item if nothing was selected
            if (drpAssemblyName.SelectedIndex < 0 && String.IsNullOrEmpty(drpAssemblyName.SelectedValue))
            {
                drpAssemblyName.SelectedIndex = 0;
            }
        }
    }

    /// <summary>
    /// Fills class name selector with data.
    /// </summary>
    private void FillClassNameSelector(string assemblyName)
    {
        // Clear items
        drpClassName.DropDownList.Items.Clear();

        if (String.IsNullOrEmpty(assemblyName))
        {
            return;
        }

        if (MacroProcessor.ContainsMacro(assemblyName))
        {
            // Include macro value if specified
            drpClassName.DropDownList.Items.Add(ClassName);
            return;
        }

        var classes = GetClassesWithAttribute(assemblyName);

        if (classes != null)
        {
            // Fill drop-down list
            foreach (string className in classes)
            {
                drpClassName.DropDownList.Items.Add(className);
            }

            // Select first item if nothing was selected
            if ((classes.Count > 0) && !AllowEmpty && (drpClassName.SelectedIndex < 0) && String.IsNullOrEmpty(drpClassName.SelectedValue))
            {
                drpClassName.SelectedIndex = 0;
            }
        }
        else
        {
            drpClassName.DropDownList.Items.Add(drpClassName.SelectedValue);
        }
    }

    private List<string> GetClassesWithAttribute(string assemblyName)
    {
        if (string.IsNullOrEmpty(BaseClassAttribute))
        {
            return ClassHelper.GetClasses(assemblyName, Settings);
        }

        string key = Settings.GetCacheKey(assemblyName) + "|" + BaseClassAttribute;

        if (assemblyClasses.ContainsKey(key))
        {
            // Get from hash table if key exists
            return assemblyClasses[key];
        }

        List<string> classList = null;

        var attributeType = Type.GetType(BaseClassAttribute);

        if (attributeType == null)
        {
            return classList;
        }

        Assembly assembly = ClassHelper.GetAssembly(assemblyName);

        // Get list of classes from selected assembly
        Type[] assemblyClassTypes;
        try
        {
            assemblyClassTypes = assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            assemblyClassTypes = exception.Types;
        }

        var classTypes = new List<Tuple<string, Type>>();

        foreach (Type classType in assemblyClassTypes.Where(x => x != null))
        {
            if (Attribute.IsDefined(classType, attributeType))
                classTypes.Add(new Tuple<string, Type>(classType.Namespace + "." + classType.Name, classType));
        }

        classList = classTypes
                        .Where(t => GetClassCondition(t.Item2, Settings))
                        .Select(t => t.Item1).Where(s => !string.IsNullOrEmpty(s))
                        .ToList();

        classList.Sort();

        assemblyClasses[key] = classList;

        return classList;
    }

    private List<string> GetAssembliesWithClassesWithAttribute()
    {
        // Get loaded assemblies
        var asms = AssemblyDiscoveryHelper.GetAssemblies(discoverableOnly: false);

        if (!String.IsNullOrEmpty(AssembliesFilter))
        {
            asms = asms.Where(t => t.ManifestModule.Name.Contains(AssembliesFilter));
        }

        var asmList = new List<string>();

        foreach (var assembly in asms)
        {
            string name = assembly.ManifestModule.Name.Replace(".dll", "");
            var classes = GetClassesWithAttribute(name);
            if ((classes == null) || (classes.Count == 0))
            {
                continue;
            }

            asmList.Add(name);
        }

        return asmList;
    }

    #region ClassHelper private members

    /// <summary>
    /// Returns visibility condition for given class type.
    /// </summary>
    /// <param name="type">Class type</param>
    /// <param name="settings">Settings used for filter list of classes</param>
    private bool GetClassCondition(Type type, ClassTypeSettings settings)
    {
        return
            !type.IsGenericType &&
            !type.IsDefined(typeof(CompilerGeneratedAttribute), false) &&
            (type.DeclaringType == null || !type.DeclaringType.IsDefined(typeof(CompilerGeneratedAttribute), false)) &&
            (!type.IsAbstract || type.IsSealed || (type.IsInterface && settings.ShowInterfaces)) &&
            // Get only public classes (because of medium trust level)
            type.IsPublic &&
            // Interface
            (!type.IsInterface || settings.ShowInterfaces) &&
            // Enumeration
            (!type.IsEnum || settings.ShowEnumerations) &&
            // Classes
            (!type.IsClass || settings.ShowClasses) &&
            // Matches base class name
            (String.IsNullOrEmpty(settings.BaseClassNames) || (ClassHelper.IsAllowed(type, settings))) &&
            // Checks whether class can be created automatically by system
            (!settings.CheckAutoCreation || TypeCanBeCreatedAutomatically(type));
    }

    /// <summary>
    /// Returns <c>true</c> if type is creatable automatically by <see cref="Activator.CreateInstance(Type)"/> method.
    /// </summary>
    private static bool TypeCanBeCreatedAutomatically(Type type)
    {
        try
        {
            return (type.GetConstructor(Type.EmptyTypes) != null);
        }
        catch
        {
            // possible reflection error is ignored and type is considered as not-creatable automatically by system
        }

        return false;
    }

    #endregion ClassHelper private members

    #endregion "Private methods"

    #region "ICallbackEventHandler"

    public string GetCallbackResult()
    {
        List<string> array = new List<string>();

        foreach (var item in drpClassName.DropDownList.Items)
        {
            array.Add(((ListItem)item).Text);
        }

        return new JavaScriptSerializer().Serialize(array);
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        string assemblyName = GetAssemblyName(eventArgument);

        FillClassNameSelector(assemblyName);
    }

    #endregion "ICallbackEventHandler"
}