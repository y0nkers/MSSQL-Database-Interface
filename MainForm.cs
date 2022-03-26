using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Data.SqlClient;

namespace CourseWork
{
    public partial class MainForm : Form
    {
        bool isAutoUpdate = true;
        BindingSource binding = new BindingSource();
        public MainForm()
        {
            InitializeComponent();
            Program.mainForm = this;
            Text = @"Учёт участия шахматистов в турнире (Текущий пользователь: " + Program.user + ")";

            categoryTableAdapter.Connection.ConnectionString = Program.connection;
            formatTableAdapter.Connection.ConnectionString = Program.connection;
            rankTableAdapter.Connection.ConnectionString = Program.connection;
            chessPlayerTableAdapter.Connection.ConnectionString = Program.connection;
            tournamentTableAdapter.Connection.ConnectionString = Program.connection;
            matchesTableAdapter.Connection.ConnectionString = Program.connection;
            tournamentResultsTableAdapter.Connection.ConnectionString = Program.connection;
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool temp = isAutoUpdate;
            isAutoUpdate = true;
            UpdateTables(sender, e);
            isAutoUpdate = temp;
        }

        private void switchAutoUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isAutoUpdate = !isAutoUpdate;
            if (isAutoUpdate)
            {
                switchAutoUpdateToolStripMenuItem.Text = "Выключить автообновление";
                toolStripStatusLabel.Text = "Автообновление информации включено.";
            }
            else
            {
                switchAutoUpdateToolStripMenuItem.Text = "Включить автообновление";
                toolStripStatusLabel.Text = "Автообновление информации выключено.";
            }
        }

        private void changeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.authForm.Show();
            Program.mainForm = null;
            Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            formatTableAdapter.Fill(chessDataSet.Format);
            categoryTableAdapter.Fill(chessDataSet.Category);
            rankTableAdapter.Fill(chessDataSet.Rank);
            chessPlayerTableAdapter.Fill(chessDataSet.ChessPlayer);
            tournamentTableAdapter.Fill(chessDataSet.Tournament);
            matchesTableAdapter.Fill(chessDataSet.Matches);
            tournamentResultsTableAdapter.Fill(chessDataSet.TournamentResults);

            chessPlayerViewTableAdapter.Fill(chessViewsDataSet.ChessPlayerView);
            tournamentViewTableAdapter.Fill(chessViewsDataSet.TournamentView);
            matchesViewTableAdapter.Fill(chessViewsDataSet.MatchesView);
            tournamentResultsViewTableAdapter.Fill(chessViewsDataSet.TournamentResultsView);

            comboBoxChooseFilter.SelectedIndex = 0;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateTables);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
        }

        private void UpdateTables(object sender, EventArgs e)
        {
            if (isAutoUpdate)
            {
                formatBindingSource.CancelEdit();
                categoryBindingSource.CancelEdit();
                rankBindingSource.CancelEdit();
                chessPlayerBindingSource.CancelEdit();
                tournamentBindingSource.CancelEdit();
                matchesBindingSource.CancelEdit();
                tournamentResultsBindingSource.CancelEdit();

                tableAdapterManager.UpdateAll(chessDataSet);
                categoryTableAdapter.Fill(chessDataSet.Category);
                formatTableAdapter.Fill(chessDataSet.Format);
                rankTableAdapter.Fill(chessDataSet.Rank);
                chessPlayerTableAdapter.Fill(chessDataSet.ChessPlayer);
                tournamentTableAdapter.Fill(chessDataSet.Tournament);
                matchesTableAdapter.Fill(chessDataSet.Matches);
                tournamentResultsTableAdapter.Fill(chessDataSet.TournamentResults);
                toolStripStatusLabel.Text = "Последнее обновление таблиц: " + DateTime.Now.ToString("HH:mm:ss");
            }
        }

        private void tabControlMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControlMain.TabPages[e.Index];
            e.Graphics.FillRectangle(new SolidBrush(page.BackColor), e.Bounds);

            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            TextRenderer.DrawText(e.Graphics, page.Text, e.Font, paddedBounds, page.ForeColor);
        }

        private void toolStripSeparator1_Paint(object sender, PaintEventArgs e)
        {
            ToolStripSeparator sep = (ToolStripSeparator)sender;

            e.Graphics.FillRectangle(new SolidBrush(Color.BurlyWood), 0, 0, sep.Width, sep.Height);

            e.Graphics.DrawLine(new Pen(Color.Black), 30, sep.Height / 2, sep.Width - 4, sep.Height / 2);
        }

        /* * * * EXCEPTIONS HANDLING * * * */

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Произошла ошибка при добавлении/изменении записи!\nПроверьте правильность введённых данных.\n\nНажмите ESC, чтобы отменить прошлое действие.", "Ошибка");
            e.Cancel = true;
        }

        private void categoryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                categoryBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(chessDataSet);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    if (ex.Message.Contains("Category_UniqueIndex_Category")) MessageBox.Show("Такая категория уже существует!", "Ошибка при добавлении/изменении категории.");
                    if (ex.Message.Contains("Category_UniqueIndex_NeededRatingELO")) MessageBox.Show("Категория с таким рейтингом ЭЛО уже существует!", "Ошибка при добавлении/изменении категории.");
                }
                if (ex is NoNullAllowedException) MessageBox.Show("Заполните все столбцы перед модифицированием информации!", "Ошибка");
            }
            finally
            {
                categoryTableAdapter.Fill(chessDataSet.Category);
                toolStripStatusLabel.Text = "Таблица категорий успешно обновлена";
                MessageBox.Show("Изменения сохранены.", "Сообщение");
            }
        }

        private void formatBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                formatBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(chessDataSet);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    if (ex.Message.Contains("Format_UniqueIndex_Title")) MessageBox.Show("Такой формат уже существует!", "Ошибка при добавлении/изменении формата.");
                }
                if (ex is NoNullAllowedException) MessageBox.Show("Заполните все столбцы перед модифицированием информации!", "Ошибка");
            }
            finally
            {
                formatTableAdapter.Fill(chessDataSet.Format);
                toolStripStatusLabel.Text = "Таблица форматов успешно обновлена";
                MessageBox.Show("Изменения сохранены.", "Сообщение");
            }
        }

        private void rankBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                rankBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(chessDataSet);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    if (ex.Message.Contains("Rank_UniqueIndex_NeededRatingELO")) MessageBox.Show("Звание с таким рейтингом ЭЛО уже существует!", "Ошибка при добавлении/изменении звания.");
                    if (ex.Message.Contains("Rank_UniqueIndex_Rank")) MessageBox.Show("Такое звание уже существует!", "Ошибка при добавлении/изменении звания.");
                }
                if (ex is NoNullAllowedException) MessageBox.Show("Заполните все столбцы перед модифицированием информации!", "Ошибка");
            }
            finally
            {
                rankTableAdapter.Fill(chessDataSet.Rank);
                toolStripStatusLabel.Text = "Таблица званий успешно обновлена";
                MessageBox.Show("Изменения сохранены.", "Сообщение");
            }
        }

        private void chessPlayerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                chessPlayerBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(chessDataSet);
            }
            catch (Exception ex)
            {
                if (ex is NoNullAllowedException) MessageBox.Show("Заполните все столбцы перед модифицированием информации!", "Ошибка");
            }
            finally
            {
                chessPlayerTableAdapter.Fill(chessDataSet.ChessPlayer);
                toolStripStatusLabel.Text = "Таблица шахматистов успешно обновлена";
                MessageBox.Show("Изменения сохранены.", "Сообщение");
            }
        }

        private void tournamentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                tournamentBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(chessDataSet);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    if (ex.Message.Contains("Tournament_UniqueIndex_Title")) MessageBox.Show("Турнир с таким названием уже существует!", "Ошибка при добавлении/изменении турнира.");
                }
                if (ex is NoNullAllowedException) MessageBox.Show("Заполните все столбцы перед модифицированием информации!", "Ошибка");
            }
            finally
            {
                tournamentTableAdapter.Fill(chessDataSet.Tournament);
                toolStripStatusLabel.Text = "Таблица турниров успешно обновлена";
                MessageBox.Show("Изменения сохранены.", "Сообщение");
            }
        }

        private void matchesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                matchesBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(chessDataSet);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    MessageBox.Show("Произошла ошибка при добавлении/изменении матча!\nПроверьте правильность введённых данных.\n" +
                        "\nВозможные проблемы:\n1. Два раза введён один и тот же шахматист." +
                        "\n2. В качестве победителя указан шахматист не из матча.", "Ошибка");
                }
                if (ex is NoNullAllowedException) MessageBox.Show("Заполните все столбцы перед модифицированием информации!", "Ошибка");
            }
            finally
            {
                matchesTableAdapter.Fill(chessDataSet.Matches);
                toolStripStatusLabel.Text = "Таблица матчей успешно обновлена";
                MessageBox.Show("Изменения сохранены.", "Сообщение");
            }
        }

        private void tournamentResultsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                tournamentBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(chessDataSet);
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    if (ex.Message.Contains("TournamentResults_UniqueIndex_PlaceTournament")) MessageBox.Show("Уже существует запись о месте в указанном турнире!", "Ошибка при добавлении/изменении результатов турнира.");
                    if (ex.Message.Contains("TournamentResults_UniqueIndex_TournamentPlayer")) MessageBox.Show("Уже существует запись об участии этого шахматиста в указанном турнире!", "Ошибка при добавлении/изменении результатов турнира.");
                    MessageBox.Show("Произошла ошибка при добавлении/изменении результата!\nПроверьте правильность введённых данных.\n" +
                        "\nВозможные проблемы:\n1. Мест не может быть больше, чем участников в турнире." +
                        "\n2. Введённый шахматист не участвовал в данном турнире.", "Ошибка");
                }
                if (ex is NoNullAllowedException) MessageBox.Show("Заполните все столбцы перед модифицированием информации!", "Ошибка");
            }
            finally
            {
                tournamentResultsTableAdapter.Fill(chessDataSet.TournamentResults);
                toolStripStatusLabel.Text = "Таблица результатов турнира успешно обновлена";
                MessageBox.Show("Изменения сохранены.", "Сообщение");
            }
        }

        /* * * * REPORTS * * * */

        private void chessPlayerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            binding.DataSource = chessViewsDataSet.ChessPlayerView;
            ReportForm f = new ReportForm("CourseWork.reports.ChessPlayerReport.rdlc", "ChessPlayerViewDataSet", binding);
            f.ShowDialog();
        }

        private void tournamentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            binding.DataSource = chessViewsDataSet.TournamentView;
            ReportForm f = new ReportForm("CourseWork.reports.TournamentReport.rdlc", "TournamentViewDataSet", binding);
            f.ShowDialog();
        }

        private void matchesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            binding.DataSource = chessViewsDataSet.MatchesView;
            ReportForm f = new ReportForm("CourseWork.reports.MatchesReport.rdlc", "MatchesViewDataSet", binding);
            f.ShowDialog();
        }

        private void tournamentResultsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            binding.DataSource = chessViewsDataSet.TournamentResultsView;
            ReportForm f = new ReportForm("CourseWork.reports.TournamentResultsReport.rdlc", "TournamentResultsViewDataSet", binding);
            f.ShowDialog();
        }

        /* * * * FILTERS * * * */

        // ChessPlayer
        private void buttonApplyRankFilter_Click(object sender, EventArgs e)
        {
            chessPlayerBindingSource.Filter = string.Format("RankID = {0}",Convert.ToInt32(comboBoxFilterRank.SelectedValue));
            int count = chessPlayerDataGridView.Rows.Count - 1;
            if (count == 1) toolStripStatusLabel.Text = "Найден " + count + " шахматист со званием " + comboBoxFilterRank.Text;
            else toolStripStatusLabel.Text = "Найдено " + count + " шахматиста(ов) со званием " + comboBoxFilterRank.Text;
        }

        private void buttonResetRankFilter_Click(object sender, EventArgs e)
        {
            chessPlayerBindingSource.RemoveFilter();
            toolStripStatusLabel.Text = "Фильтр шахматистов по званию сброшен.";
        }

        // Tournament
        private void buttonApplyFormatFilter_Click(object sender, EventArgs e)
        {
            tournamentBindingSource.Filter = string.Format("FormatID = {0}", Convert.ToInt32(comboBoxFilterFormat.SelectedValue));
            int count = tournamentDataGridView.Rows.Count - 1;
            if (count == 1) toolStripStatusLabel.Text = "Найден " + count + " турнир формата " + comboBoxFilterFormat.Text;
            else toolStripStatusLabel.Text = "Найдено " + count + " турнира(ов) формата " + comboBoxFilterFormat.Text;
        }

        private void buttonResetFormatFilter_Click(object sender, EventArgs e)
        {
            tournamentBindingSource.RemoveFilter();
            toolStripStatusLabel.Text = "Фильтр турниров по формату сброшен.";
        }

        // Matches
        private void buttonApplyTournamentFilter_Click(object sender, EventArgs e)
        {
            matchesBindingSource.Filter = string.Format("TournamentID = {0}", Convert.ToInt32(comboBoxFilterTournament.SelectedValue));
            int count = matchesDataGridView.Rows.Count - 1;
            if (count == 1) toolStripStatusLabel.Text = "Найден " + count + " матч из турнира " + comboBoxFilterTournament.Text;
            else toolStripStatusLabel.Text = "Найдено " + count + " матча(ей) из турнира " + comboBoxFilterTournament.Text;
        }

        private void buttonResetTournamentFilter_Click(object sender, EventArgs e)
        {
            matchesBindingSource.RemoveFilter();
            toolStripStatusLabel.Text = "Фильтр матчей по турниру сброшен.";
        }

        // TournamentResults
        private void comboBoxChooseFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxChooseFilter.SelectedIndex == 0)
            {
                comboBoxFilter.DataSource = tournamentBindingSource;
                comboBoxFilter.ValueMember = "TournamentID";
                comboBoxFilter.DisplayMember = "Title";
            }
            else
            {
                comboBoxFilter.DataSource = chessPlayerBindingSource;
                comboBoxFilter.ValueMember = "ChessPlayerID";
                comboBoxFilter.DisplayMember = "LastName";
            }
        }

        private void buttonApplyTournamentResultsFilter_Click(object sender, EventArgs e)
        {
            if (comboBoxChooseFilter.SelectedIndex == 0)
            {
                tournamentResultsBindingSource.Filter = string.Format("TournamentID = {0}", Convert.ToInt32(comboBoxFilter.SelectedValue));
                int count = tournamentResultsDataGridView.Rows.Count - 1;
                if (count == 1) toolStripStatusLabel.Text = "Найден " + count + " результат из турнира " + comboBoxFilter.Text;
                else toolStripStatusLabel.Text = "Найдено " + count + " результата(ов) из турнира " + comboBoxFilter.Text;
            }
            else
            {
                tournamentResultsBindingSource.Filter = string.Format("PlayerID = {0}", Convert.ToInt32(comboBoxFilter.SelectedValue));
                int count = tournamentResultsDataGridView.Rows.Count - 1;
                if (count == 1) toolStripStatusLabel.Text = "Найден " + count + " результат шахматиста " + comboBoxFilter.Text;
                else toolStripStatusLabel.Text = "Найдено " + count + " результата(ов) шахматиста " + comboBoxFilter.Text;
            }
        }

        private void buttonResetTournamentResultsFilter_Click(object sender, EventArgs e)
        {
            tournamentResultsBindingSource.RemoveFilter();
            if (comboBoxChooseFilter.SelectedIndex == 0) toolStripStatusLabel.Text = "Фильтр результатов по турниру сброшен.";
            else toolStripStatusLabel.Text = "Фильтр результатов по шахматисту сброшен.";
        }

    }
}
