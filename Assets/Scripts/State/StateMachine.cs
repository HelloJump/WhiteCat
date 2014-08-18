using UnityEngine;
using System.Collections;

public class StateMachine<T>
{
    T owner;
    AbsState<T> currentState;
    AbsState<T> previousState;
    AbsState<T> globalState;

    public StateMachine(T owner)
    {
        this.owner = owner;
        this.currentState = null;
        this.previousState = null;
        this.globalState = null;
    }

    public void SetCurrentState(AbsState<T> state)
    {
        this.currentState = state;
    }

    public void SetGlobalState(AbsState<T> state)
    {
        this.globalState = state;
    }

    public void SetPreviousState(AbsState<T> state)
    {
        this.previousState = state;
    }

    public void UpdateState()
    {
        if (this.globalState != null)
        {
            this.globalState.Execute(owner);
        }

        if (this.currentState != null)
        {
            this.currentState.Execute(owner);
        }
    }

    public void ChangeState(AbsState<T> newState)
    {
        if (newState == null)
            Debug.LogError("Change To State null");
        if (newState == currentState)
            Debug.LogWarning("newState == currentState");
        this.previousState = this.currentState;
        this.currentState.Exit(owner);
        this.currentState = newState;
        this.currentState.Enter(owner);
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

    public AbsState<T> GetCurrentState()
    {
        return this.currentState;
    }

    public AbsState<T> GetGlobalState()
    {
        return this.globalState;
    }

    public AbsState<T> GetPreviousState()
    {
        return this.previousState;
    }

    public bool IsInState(AbsState<T> state)
    {
        return this.currentState == state;
    }
}
