# Source Control System – Design Patterns Project (Fall 2024)

## Project Description

This project simulates the functionality of a Source Control System similar to GIT, focusing on implementing key design patterns. The system allows managing branches, files, and folders, tracking the status of files, and maintaining a history of changes, all while using the **Composite**, **State**, **Prototype**, and **Observer** patterns.

## Features

1. **Branch Management**
   - Users can create, delete, and manage branches.
   - Each branch can hold a unique or shared set of files and folders in a hierarchical structure.
   - The branch system is implemented using the **Composite** design pattern to represent the hierarchy of files and folders.

2. **File Operations & States**
   - Users can modify or add files to branches.
   - Each file can be in one of several states:
     - **Draft**
     - **Staged**
     - **Committed**
     - **Under Review**
     - **Ready to Merge**
     - **Merged**
   - The **State** design pattern ensures that transitions between these states are controlled, preventing illegal state transitions (e.g., files cannot be merged without review).

3. **Commit and Merge**
   - Files can be committed to branches in groups rather than individually.
   - Users can merge branches, with the system ensuring proper handling of file conflicts and status.

4. **Branch Cloning**
   - Users can create new branches from existing ones using the **Prototype** design pattern, which ensures that all files and folder structures are cloned.

5. **Review Notifications**
   - When a file is submitted for review, an automatic notification is sent to reviewers using the **Observer** design pattern. This feature ensures that all designated reviewers are informed of the file’s status and need for review.

6. **History Tracking**
   - The system tracks the history of each file across branches.
   - Users can view the history of changes and revert files to previous versions if needed.

## Design Patterns Used

- **Composite**: For managing the hierarchical folder and file structure in each branch.
- **State**: For controlling the file state transitions (Draft, Staged, Committed, etc.).
- **Prototype**: To enable cloning of existing branches into new ones.
- **Observer**: For notifying reviewers when a file is ready for review.

## Project Structure

The project consists of the following key components:

- **Branch**: Manages the files and folders within a branch.
- **File**: Represents an individual file with a state (e.g., Draft, Staged).
- **Folder**: Holds a collection of files or other folders.
- **State Classes**: Implement the behavior of each file state (e.g., DraftState, StagedState).
- **Review System**: Notifies reviewers when a file needs to be reviewed.

## How to Use

1. **Creating a Branch**: Use the `createBranch()` function to clone an existing branch or create a new one from scratch.
2. **Adding Files**: Files can be added or modified within any branch. The system tracks changes to each file.
3. **Committing Changes**: Use the `commit()` function to save changes across multiple files in a branch.
4. **Merging Branches**: The `merge()` function allows you to integrate changes from one branch into another.
5. **Reviewing Files**: Files can be submitted for review, and reviewers will be notified automatically.
6. **Viewing History**: You can request the history of changes for any file and restore previous versions as needed.
