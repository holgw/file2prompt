![Header](/docs/header.png)

**file2prompt** is a simple desktop application built with C#, .NET, and Avalonia UI. Its purpose is to simplify the mass processing of text files using LLMs via the OpenAI API.

## 📝 Description

The application allows users to process multiple text files from a specified directory by combining their content with a customizable prompt template. Each file's content is sent individually to an LLM (via OpenAI API) in separate dialogue, and the responses are saved in a structured output folder, mirroring the input folder structure.

![Header](/docs/screenshot.png)

## 🚀 Features

- **Presets Management**  
  Save, load, and delete preset configurations (API settings + prompt template) for quick reuse.

- **Flexible File Matching**  
  Use regular expressions to filter which files should be processed.

- **Template Prompt Support**  
  Combine file content with a custom prompt template using placeholders.

- **Structured Output**  
  Results are saved in the same folder structure as input files, with optional suffixes.

## 📥 Installation

1. Download the latest release from the [Releases page](https://github.com/holgw/file2prompt/releases).
2. Extract the archive to a folder of your choice.
3. Run `file2prompt.exe`

## 🧠 Tips

- Use the "Save New" button to create reusable presets for common tasks.
- Add `[BODY]` placeholder in your prompt templates to inject file content.
- You can use **Start Tag** and **End Tag** to extract specific parts from LLM output.

## 🙌 Contributing

Contributions are welcome! Feel free to open issues or submit pull requests on GitHub.

---

> If you like this tool, please consider giving it a ⭐ on GitHub!