using System.Collections.Generic;
using Ink.Runtime;

namespace Dialog
{
    public class InkDialogueVariables
    {
        private readonly Dictionary<string, Object> _variables;

        public InkDialogueVariables(Story story) 
        {
            // initialize the dictionary using the global variables in the story
            _variables = new Dictionary<string, Object>();
            foreach (string name in story.variablesState)
            {
                Object value = story.variablesState.GetVariableWithName(name);
                _variables.Add(name, value);
                //Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
            }
        }

        public void SyncVariablesAndStartListening(Story story) 
        {
            // it's important that SyncVariablesToStory is before assigning the listener!
            SyncVariablesToStory(story);
            story.variablesState.variableChangedEvent += UpdateVariableState;
        }

        public void StopListening(Story story)
        {
            story.variablesState.variableChangedEvent -= UpdateVariableState;
        }

        public void UpdateVariableState(string name, Object value)
        {
            // only maintain variables that were initialized from the globals ink file
            if (!_variables.ContainsKey(name)) 
            { 
                return; 
            }
            _variables[name] = value;
            //Debug.Log("Updated dialogue variable: " + name + " = " + value);
        }

        private void SyncVariablesToStory(Story story)
        {
            foreach (KeyValuePair<string, Object> variable in _variables)
            {
                story.variablesState.SetGlobal(variable.Key, variable.Value);
            }
        }
    }
}