using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;

namespace AudioRecord
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadMicrophones();
            checkBoxListen.CheckedChanged += CheckBoxListen_CheckedChanged;

            recordings = new List<MemoryStream>();
            isRecording = false;

            button1.Click += Button1_Click; // 开始录音
            button2.Click += Button2_Click; // 暂停/继续录音
            button3.Click += Button3_Click; // 停止录音

            UpdateStatus("未录制"); // 初始状态

            // 注册 KeyDown 事件
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
        }

        private WaveInEvent waveIn;
        private WaveOutEvent waveOut;
        private BufferedWaveProvider waveProvider;
        private bool isRecording;
        private List<MemoryStream> recordings; // 存储录音数据

        private void LoadMicrophones()
        {
            int deviceCount = WaveIn.DeviceCount;
            comboBoxMicrophones.Items.Clear();

            for (int i = 0; i < deviceCount; i++)
            {
                var deviceInfo = WaveIn.GetCapabilities(i);
                comboBoxMicrophones.Items.Add($"{deviceInfo.ProductName} (Device {i})");
            }

            if (comboBoxMicrophones.Items.Count > 0)
            {
                comboBoxMicrophones.SelectedIndex = 0; // 默认选择第一个
            }
        }

        private void CheckBoxListen_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxListen.Checked)
            {
                StartListening();
            }
            else
            {
                StopListening();
            }
        }

        private void StartListening()
        {
            if (waveIn != null)
            {
                StopListening();
            }

            int selectedDevice = comboBoxMicrophones.SelectedIndex;
            waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(44100, 1) // 设置音频格式，44100Hz 单声道
            };

            waveProvider = new BufferedWaveProvider(waveIn.WaveFormat);
            waveOut = new WaveOutEvent();
            waveOut.Init(waveProvider);

            waveIn.DataAvailable += (s, e) =>
            {
                waveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
            };

            waveIn.DeviceNumber = selectedDevice;
            waveIn.StartRecording();
            waveOut.Play();
        }

        private void StopListening()
        {
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }

            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StartRecording();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (isRecording)
            {
                PauseRecording();
            }
            else
            {
                ResumeRecording();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            StopRecording();
        }

        private void StartRecording()
        {
            if (waveIn != null) return; // 如果已经在录音则返回

            int selectedDevice = comboBoxMicrophones.SelectedIndex;
            waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(44100, 1) // 设置音频格式
            };

            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.DeviceNumber = selectedDevice;
            waveIn.StartRecording();
            isRecording = true;

            UpdateStatus("录制中"); // 更新状态
        }

        private void PauseRecording()
        {
            if (waveIn != null && isRecording)
            {
                waveIn.StopRecording(); // 暂停录音
                isRecording = false;
                UpdateStatus("暂停中"); // 更新状态
            }
        }

        private void ResumeRecording()
        {
            if (waveIn != null && !isRecording)
            {
                waveIn.StartRecording(); // 继续录音
                isRecording = true;
                UpdateStatus("录制中"); // 更新状态
            }
        }

        private void StopRecording()
        {
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;

                // 保存录音到桌面
                SaveRecordingToDesktop();
                UpdateStatus("未录制"); // 更新状态
            }
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            // 将录音数据添加到缓冲区
            var recordingStream = new MemoryStream();
            recordingStream.Write(e.Buffer, 0, e.BytesRecorded);
            recordings.Add(recordingStream); // 保存录音数据
        }

        private void SaveRecordingToDesktop()
        {
            try
            {
                // 获取桌面的路径
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = $"Recording_{DateTime.Now:yyyyMMdd_HHmmss}.wav";
                string filePath = Path.Combine(desktopPath, fileName);

                // 创建 WAV 文件格式
                using (var outputStream = new FileStream(filePath, FileMode.Create))
                {
                    using (var writer = new WaveFileWriter(outputStream, new WaveFormat(44100, 1)))
                    {
                        foreach (var recording in recordings)
                        {
                            recording.Position = 0; // 重置流位置
                            recording.CopyTo(writer);
                        }
                    }
                }

                MessageBox.Show($"录音已保存到桌面: {fileName}");
                recordings.Clear(); // 清空记录
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存录音失败: {ex.Message}");
            }
        }

        private void UpdateStatus(string status)
        {
            label1.Text = status; // 更新 label1 的文本
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            // 检测空格键按下
            if (e.KeyCode == Keys.Space)
            {
                Button2_Click(this, EventArgs.Empty); // 调用 Button2_Click 方法
                e.Handled = true; // 防止其他处理
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // 空格键抬起时的处理，可以不需要其他逻辑

        }
    }
}
